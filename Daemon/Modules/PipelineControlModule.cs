using Daemon.Entities.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using Daemon.Helpers;

namespace Daemon.Modules {
    /// <summary>本地(命名管道)管理模块</summary>
    public class PipelineControlModule:IDisposable {
        public Boolean Useable{get;private set;}=false;

        /// <summary>命名管道</summary>
        private NamedPipeServerStream NamedPipeServerStream{get;set;}=null;
        /// <summary>命名管道名称</summary>
        private String PipelineName{get;set;}=null;
        /// <summary>取消句柄</summary>
        private CancellationTokenSource CancellationTokenSource{get;set;}=null;

        #region IDisposable
        private bool disposedValue;

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: 释放托管状态(托管对象)
                    this.NamedPipeServerStream.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                disposedValue=true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~PipelineControlModule()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose() {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="pipelineName"></param>
        /// <returns></returns>
        public Boolean Setup(String pipelineName) {
            if(String.IsNullOrWhiteSpace(pipelineName)){return false;}
            this.PipelineName=pipelineName;
            this.CancellationTokenSource=new CancellationTokenSource();
            Task.Run(()=>{
                //10秒后才接收指令
                SpinWait.SpinUntil(()=>false,10000);
                this.Useable=true;
            });
            //完成
            return true;
        }

        public void StartServer(){
            Task.Run(()=>{
                while(!this.CancellationTokenSource.IsCancellationRequested) {
                    try {
                        //创建命名管道
                        this.NamedPipeServerStream=new NamedPipeServerStream(this.PipelineName,PipeDirection.InOut,1,PipeTransmissionMode.Message,PipeOptions.Asynchronous|PipeOptions.WriteThrough);
                        //等待链接
                        this.NamedPipeServerStream.WaitForConnection();
                        //收到消息,104字节应该够了,8字节头部,96(32*3)字节字符串
                        Byte[] buffer=new Byte[104];
                        this.NamedPipeServerStream.Read(buffer,0,104);
                        Byte[] responseBytes=this.OnMessage(buffer);
                        //回复消息
                        this.NamedPipeServerStream.Write(responseBytes);
                        this.NamedPipeServerStream.Flush();
                        //释放
                        this.NamedPipeServerStream.Dispose();
                    }catch(Exception exception) {
                        Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.StartServer[Error]",$"命名管道异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                    }
                    SpinWait.SpinUntil(()=>false,1000);
                }
            },this.CancellationTokenSource.Token);
        }

        public void StopServer(){
            this.CancellationTokenSource.Cancel();
        }

        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private Byte[] OnMessage(Byte[] bytes){
            if(!this.Useable){return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00};}
            switch(bytes[0]) {
                case 0x01:return OnMessageStatus(bytes);
                case 0x02:return OnMessageStart(bytes);
                case 0x03:return OnMessageStop(bytes);
                case 0x04:return OnMessageRestart(bytes);
                case 0x05:return OnMessageLoad(bytes);
                case 0x06:return OnMessageRemove(bytes);
                case 0x12:return OnMessageStartAll();
                case 0x13:return OnMessageStopAll();
                case 0x14:return OnMessageRestartAll();
                case 0x15:return OnMessageLoadAll();
                case 0x16:return OnMessageRemoveAll();
                case 0xF0:return OnMessageDaemonVersion();
                case 0xFF:return OnMessageDaemonShutdown();
                default:
                    Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessage","命名管道收到未知指令");
                    return new Byte[23]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x75,0x6E,0x6B,0x6E,0x6F,0x77,0x6E,0x20,0x63,0x6F,0x6D,0x6D,0x61,0x6E,0x64};
            }
        }

        /// <summary>
        /// 处理消息 0x01
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static Byte[] OnMessageStatus(Byte[] bytes){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStatus","开始处理 status 指令");
            //解析 unitKey
            String unitKey;
            try {
                unitKey=Encoding.UTF8.GetString(bytes,8,104-8).TrimEnd().TrimEnd(new Char[]{'\0'});
            }catch(Exception exception){
                Helpers.LoggerModuleHelper.TryLog(
                    "Modules.PipelineControlModule.OnMessageStatus[Error]",
                    $"解析 unitKey 异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            if(String.IsNullOrWhiteSpace(unitKey)){
                Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStatus[Error]","解析 unitKey 失败");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable process command")).ToArray();
            }
            Unit unit=Program.UnitManageModule.GetUnit(unitKey);
            if(unit==null) {
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unit is not found")).ToArray();
            }
            //拼接返回数据
            StringBuilder unitStatusText=new StringBuilder();
            UnitSettings unitSettings=unit.State==2?unit.RunningSettings:unit.Settings;
            //第一行
            switch(unit.State){
                case 1:unitStatusText.Append("§b●§| ");break;
                case 2:unitStatusText.Append("§2●§| ");break;
                default:unitStatusText.Append("● ");break;
            }
            unitStatusText.Append(unitKey).Append(" - ").Append(unitSettings.Description);
            //第二行
            unitStatusText.Append("\n     Loaded:  ").Append(Program.AppEnvironment.UnitsDirectory).Append(Path.DirectorySeparatorChar).Append(unitKey).Append(".json;    ");
            unitStatusText.Append(unitSettings.AutoStart?"enabled":"disabled");
            //第三行
            unitStatusText.Append("\n      State:  ");
            switch(unit.State) {
                case 0:unitStatusText.Append("stopped");break;
                case 1:unitStatusText.Append("§bstarting§|");break;
                case 2:
                    unitStatusText.Append("§2started§| §b#").Append(unit.Process.Id).Append("§|");
                    break;
                case 3:unitStatusText.Append("stopping");break;
                default:unitStatusText.Append("unknown");break;
            }
            if(unit.State==2 && unit.Process!=null) {
                String datetime=unit.Process.StartTime.ToString("yyyy-MM-dd HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture);
                unitStatusText.Append(" (since ").Append(datetime).Append(")");
            }
            //第四行
            unitStatusText.Append("\nCommandLine:  ").Append(unitSettings.AbsoluteExecutePath);
            if(!String.IsNullOrWhiteSpace(unitSettings.Arguments)){ unitStatusText.Append(' ').Append(unitSettings.Arguments); }
            //第五行
            if(unit.State==2 && unitSettings.MonitorPerformanceUsage && Program.UnitPerformanceCounterModule.Useable) {
                String cpuValue=String.Format(CultureInfo.InvariantCulture,"{0:N1} %",Program.UnitPerformanceCounterModule.GetCpuValue(unitKey));
                String ramValue=Program.UnitPerformanceCounterModule.GetRamValue(unitKey).FixedByteSize();
                unitStatusText.Append("\nPerformance:  ").Append(cpuValue).Append(";    ").Append(ramValue);
            }
            //第六行
            if(unit.State==2 && unitSettings.MonitorNetworkUsage && Program.UnitNetworkCounterModule.Useable) {
                UnitNetworkCounter unitNetworkCounter=Program.UnitNetworkCounterModule.GetValue(unit.ProcessId);
                if(unitNetworkCounter!=null) {
                    unitStatusText.Append("\n    Network:  ")
                    .Append('↑').Append(unitNetworkCounter.TotalSent.FixedByteSize()).Append(" @ ").Append(unitNetworkCounter.SendSpeed.FixedByteSize()).Append("/s;    ")
                    .Append("↓").Append(unitNetworkCounter.TotalReceived.FixedByteSize()).Append(" @ ").Append(unitNetworkCounter.ReceiveSpeed.FixedByteSize()).Append("/s");
                }
            }
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStatus","已处理 status 指令");
            return new Byte[8]{0x01,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes(unitStatusText.ToString())).ToArray();
        }
        /// <summary>
        /// 处理消息 0x02
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static Byte[] OnMessageStart(Byte[] bytes){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStart","开始处理 start 指令");
            //解析 unitKey
            String unitKey;
            try {
                unitKey=Encoding.UTF8.GetString(bytes,8,104-8).TrimEnd().TrimEnd(new Char[]{'\0'});
            }catch(Exception exception){
                Helpers.LoggerModuleHelper.TryLog(
                    "Modules.PipelineControlModule.OnMessageStart[Error]",
                    $"解析 unitKey 异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            if(String.IsNullOrWhiteSpace(unitKey)){
                Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStart[Error]","解析 unitKey 失败");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Unit unit=Program.UnitManageModule.GetUnit(unitKey);
            if(unit==null) {
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unit is not found")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.StartUnit(unitKey,false);
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStart","已处理 start 指令");
            return new Byte[8]{0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x03
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static Byte[] OnMessageStop(Byte[] bytes){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStop","开始处理 stop 指令");
            //解析 unitKey
            String unitKey;
            try {
                unitKey=Encoding.UTF8.GetString(bytes,8,104-8).TrimEnd().TrimEnd(new Char[]{'\0'});
            }catch(Exception exception){
                Helpers.LoggerModuleHelper.TryLog(
                    "Modules.PipelineControlModule.OnMessageStop[Error]",
                    $"解析 unitKey 异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            if(String.IsNullOrWhiteSpace(unitKey)){
                Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStop[Error]","解析 unitKey 失败");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Unit unit=Program.UnitManageModule.GetUnit(unitKey);
            if(unit==null) {
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unit is not found")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.StopUnit(unitKey);
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStop","已处理 stop 指令");
            return new Byte[8]{0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x04
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static Byte[] OnMessageRestart(Byte[] bytes){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRestart","开始处理 restart 指令");
            //解析 unitKey
            String unitKey;
            try {
                unitKey=Encoding.UTF8.GetString(bytes,8,104-8).TrimEnd().TrimEnd(new Char[]{'\0'});
            }catch(Exception exception){
                Helpers.LoggerModuleHelper.TryLog(
                    "Modules.PipelineControlModule.OnMessageRestart[Error]",
                    $"解析 unitKey 异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            if(String.IsNullOrWhiteSpace(unitKey)){
                Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRestart[Error]","解析 unitKey 失败");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Unit unit=Program.UnitManageModule.GetUnit(unitKey);
            if(unit==null) {
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unit is not found")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.RestartUnit(unitKey);
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRestart","已处理 restart 指令");
            return new Byte[8]{0x04,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x05
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static Byte[] OnMessageLoad(Byte[] bytes){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageLoad","开始处理 load 指令");
            //解析 unitKey
            String unitKey;
            try {
                unitKey=Encoding.UTF8.GetString(bytes,8,104-8).TrimEnd().TrimEnd(new Char[]{'\0'});
            }catch(Exception exception){
                Helpers.LoggerModuleHelper.TryLog(
                    "Modules.PipelineControlModule.OnMessageLoad[Error]",
                    $"解析 unitKey 异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            if(String.IsNullOrWhiteSpace(unitKey)){
                Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageLoad[Error]","解析 unitKey 失败");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Boolean b1=Program.UnitManageModule.LoadUnit(unitKey);
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageLoad","已处理 load 指令");
            return new Byte[8]{0x05,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes($"unit load {(b1?"success":"fail")}")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x06
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static Byte[] OnMessageRemove(Byte[] bytes){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRemove","开始处理 remove 指令");
            //解析 unitKey
            String unitKey;
            try {
                unitKey=Encoding.UTF8.GetString(bytes,8,104-8).TrimEnd().TrimEnd(new Char[]{'\0'});
            }catch(Exception exception){
                Helpers.LoggerModuleHelper.TryLog(
                    "Modules.PipelineControlModule.OnMessageRemove[Error]",
                    $"解析 unitKey 异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            if(String.IsNullOrWhiteSpace(unitKey)){
                Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRemove[Error]","解析 unitKey 失败");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            if(Program.UnitManageModule.GetUnit(unitKey)==null) {
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unit is not found")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.RemoveUnit(unitKey);
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRemove","已处理 remove 指令");
            return new Byte[8]{0x06,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }

        /// <summary>
        /// 处理消息 0x12
        /// </summary>
        /// <returns></returns>
        private static Byte[] OnMessageStartAll(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStartAll","开始处理 start-all 指令");
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.StartAllUnits(false);
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStartAll","已处理 start-all 指令");
            return new Byte[8]{0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x13
        /// </summary>
        /// <returns></returns>
        private static Byte[] OnMessageStopAll(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStopAll","开始处理 stop-all 指令");
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.StopAllUnits();
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStopAll","已处理 stop 指令");
            return new Byte[8]{0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x14
        /// </summary>
        /// <returns></returns>
        private static Byte[] OnMessageRestartAll(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRestartAll","开始处理 restart-all 指令");
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.RestartAllUnits();
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRestartAll","已处理 restart-all 指令");
            return new Byte[8]{0x04,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x15
        /// </summary>
        /// <returns></returns>
        private static Byte[] OnMessageLoadAll(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageLoadAll","开始处理 load-all 指令");
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Int32 count=Program.UnitManageModule.LoadAllUnits();
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageLoadAll","已处理 load-all 指令");
            return new Byte[8]{0x05,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes($"loaded {count} unit")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x16
        /// </summary>
        /// <returns></returns>
        private static Byte[] OnMessageRemoveAll(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRemoveAll","开始处理 remove-all 指令");
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.RemoveAllUnits();
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRemoveAll","已处理 remove-all 指令");
            return new Byte[8]{0x06,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }


        /// <summary>
        /// 处理消息 0xF0
        /// </summary>
        /// <returns></returns>
        private static Byte[] OnMessageDaemonVersion(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageDaemonVersion","开始处理 daemon-version 指令");
            Version version=Assembly.GetExecutingAssembly().GetName().Version;
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageDaemonVersion","已处理 daemon-version 指令");
            return new Byte[8]{0xF0,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("Wind Daemon v"+version.ToString())).ToArray();
        }
        /// <summary>
        /// 处理消息 0xFF
        /// </summary>
        /// <returns></returns>
        private static Byte[] OnMessageDaemonShutdown(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageDaemonShutdown","开始处理 daemon-shutdown 指令");
            Task.Run(()=>{
                SpinWait.SpinUntil(()=>false,1000);
                Program.DaemonServiceController.Stop();
                SpinWait.SpinUntil(()=>false,2000);
                Environment.Exit(0);
            });
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageDaemonShutdown","已处理 daemon-shutdown 指令");
            return new Byte[8]{0xFF,0x00,0x00,0x00,0x00,0x00,0x00,0x00};
        }
    }
}
