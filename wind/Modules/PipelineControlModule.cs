using Google.Protobuf;
using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using wind.Entities.Common;
using wind.Entities.Protobuf;
using wind.Helpers;

namespace wind.Modules {
    /// <summary>本地(命名管道)管理模块</summary>
    public class PipelineControlModule:IDisposable {
        public Boolean Useable{get;private set;}=false;

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
            //完成
            this.Useable=true;
            return true;
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void StartServer(){
            Task.Run(()=>{
                //10秒后才接收指令
                #if !DEBUG
                SpinWait.SpinUntil(()=>false,10000);
                #endif
                //
                while(!this.CancellationTokenSource.IsCancellationRequested) {
                    NamedPipeServerStream namedPipeServerStream=null;
                    try {
                        //创建命名管道
                        namedPipeServerStream=new NamedPipeServerStream(
                            this.PipelineName,PipeDirection.InOut,1,PipeTransmissionMode.Byte,PipeOptions.Asynchronous|PipeOptions.WriteThrough);
                        namedPipeServerStream.WaitForConnection();
                        //收到消息
                        Byte[] buffer=new Byte[100];//4字节指令,最多96字节(32个utf8字符)的参数
                        Int32 bufferSize=namedPipeServerStream.Read(buffer,0,100);
                        Byte[] response=OnMessage(buffer,bufferSize);
                        //回复消息
                        namedPipeServerStream.Write(response);
                        namedPipeServerStream.Flush();
                    }catch(Exception exception) {
                        Helpers.LoggerModuleHelper.TryLog(
                            "Modules.PipelineControlModule.StartServer[Error]",$"命名管道异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                    }finally{
                        #if !DEBUG
                        SpinWait.SpinUntil(()=>false,1000);
                        #endif
                        namedPipeServerStream?.Dispose();
                    }
                }
            },this.CancellationTokenSource.Token);
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void StopServer()=>this.CancellationTokenSource.Cancel();

        /// <summary>
        /// 消息分发处理
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        private static Byte[] OnMessage(Byte[] buffer,Int32 bufferSize){
            if(bufferSize<1){return buffer;}
            Byte[] request=buffer.AsSpan(0,bufferSize).ToArray();
            PacketTestProtobuf packetTestProtobuf=PacketTestProtobuf.Parser.ParseFrom(request);
            switch(packetTestProtobuf.Type){
                case 1001:return OnStatus(request);
                case 1200:return OnDaemonVersion();
                case 1201:return OnDaemonStatus();
                case 1299:return OnDaemonShutdown();
                default:return buffer;
            }
        }

        /// <summary>
        /// windctl status unitKey
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static Byte[] OnStatus(Byte[] request){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnStatus","开始处理 status 指令");
            StatusRequestProtobuf statusRequestProtobuf=StatusRequestProtobuf.Parser.ParseFrom(request);
            StatusResponseProtobuf statusResponseProtobuf=new StatusResponseProtobuf{Type=2001,UnitKey=statusRequestProtobuf.UnitKey};
            //无效unit
            if(String.IsNullOrWhiteSpace(statusRequestProtobuf.UnitKey)){
                statusResponseProtobuf.NoExecuteMessage="unitKey invalid";
                return statusResponseProtobuf.ToByteArray();
            }
            if(!Program.UnitManageModule.Useable){
                statusResponseProtobuf.NoExecuteMessage="unit manager not available";
                return statusResponseProtobuf.ToByteArray();
            }
            Entities.Common.Unit unit=Program.UnitManageModule.GetUnit(statusRequestProtobuf.UnitKey);
            if(unit==null) {
                statusResponseProtobuf.NoExecuteMessage="unit not found";
                return statusResponseProtobuf.ToByteArray();
            }
            //
            ProcessProtobuf processProtobuf=new ProcessProtobuf();
            if(unit.State==2) {
                processProtobuf.Id=unit.ProcessId;
                processProtobuf.StartTime=unit.Process.StartTime.ToLocalTimestamp();//这里的starttime是localtime
            }
            UnitSettingsProtobuf unitSettingsProtobuf=new UnitSettingsProtobuf{
                Name=unit.Settings.Name,Description=unit.Settings.Description,Type=unit.Settings.Type,AbsoluteExecutePath=unit.Settings.AbsoluteExecutePath,
                AbsoluteWorkDirectory=unit.Settings.AbsoluteWorkDirectory,Arguments=String.IsNullOrWhiteSpace(unit.Settings.Arguments)?String.Empty:unit.Settings.Arguments,
                AutoStart=unit.Settings.AutoStart,AutoStartDelay=unit.Settings.AutoStartDelay,RestartWhenException=unit.Settings.RestartWhenException,
                MonitorPerformanceUsage=unit.Settings.MonitorPerformanceUsage,MonitorNetworkUsage=unit.Settings.MonitorNetworkUsage};
            UnitSettingsProtobuf unitRunningSettingsProtobuf=new UnitSettingsProtobuf();
            if(unit.State==2){
                unitRunningSettingsProtobuf=new UnitSettingsProtobuf{
                    Name=unit.RunningSettings.Name,Description=unit.RunningSettings.Description,Type=unit.RunningSettings.Type,
                    AbsoluteExecutePath=unit.RunningSettings.AbsoluteExecutePath,AbsoluteWorkDirectory=unit.RunningSettings.AbsoluteWorkDirectory,
                    Arguments=String.IsNullOrWhiteSpace(unit.RunningSettings.Arguments)?String.Empty:unit.RunningSettings.Arguments,
                    AutoStart=unit.RunningSettings.AutoStart,AutoStartDelay=unit.RunningSettings.AutoStartDelay,
                    RestartWhenException=unit.RunningSettings.RestartWhenException,MonitorPerformanceUsage=unit.RunningSettings.MonitorPerformanceUsage,
                    MonitorNetworkUsage=unit.RunningSettings.MonitorNetworkUsage};
            }            
            UnitPerformanceCounterProtobuf unitPerformanceCounterProtobuf=new UnitPerformanceCounterProtobuf();
            if(Program.UnitPerformanceCounterModule.Useable && unit.State==2 && unitRunningSettingsProtobuf.MonitorPerformanceUsage){
                unitPerformanceCounterProtobuf.CPU=Program.UnitPerformanceCounterModule.GetCpuValue(unit.ProcessId);
                unitPerformanceCounterProtobuf.RAM=Program.UnitPerformanceCounterModule.GetRamValue(unit.ProcessId);
            }
            UnitNetworkCounterProtobuf unitNetworkCounterProtobuf=new UnitNetworkCounterProtobuf();
            if(Program.UnitNetworkCounterModule.Useable && unit.State==2 && unitRunningSettingsProtobuf.MonitorNetworkUsage){
                UnitNetworkCounter unitNetworkCounter=Program.UnitNetworkCounterModule.GetValue(unit.ProcessId);
                if(unitNetworkCounter!=null){
                    unitNetworkCounterProtobuf.SendSpeed=unitNetworkCounter.SendSpeed;
                    unitNetworkCounterProtobuf.ReceiveSpeed=unitNetworkCounter.ReceiveSpeed;
                    unitNetworkCounterProtobuf.TotalSent=unitNetworkCounter.TotalSent;
                    unitNetworkCounterProtobuf.TotalReceived=unitNetworkCounter.TotalReceived;
                }
            }
            UnitProtobuf unitProtobuf=new UnitProtobuf{
                Key=unit.Key,State=unit.State,
                SettingsFilePath=String.Concat(Program.AppEnvironment.UnitsDirectory,Path.DirectorySeparatorChar,unit.Key,".json"),
                ProcessProtobuf=processProtobuf,SettingsProtobuf=unitSettingsProtobuf,RunningSettingsProtobuf=unitRunningSettingsProtobuf,
                PerformanceCounterProtobuf=unitPerformanceCounterProtobuf,NetworkCounterProtobuf=unitNetworkCounterProtobuf};
            statusResponseProtobuf.Executed=true;
            statusResponseProtobuf.UnitProtobuf=unitProtobuf;
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnStatus","已处理 status 指令");
            return statusResponseProtobuf.ToByteArray();
        }

        /// <summary>
        /// windctl daemon-version
        /// </summary>
        /// <returns></returns>
        private static Byte[] OnDaemonVersion(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnDaemonVersion","开始处理 daemon-version 指令");
            Version version=Assembly.GetExecutingAssembly().GetName().Version;
            DaemonVersionResponseProtobuf daemonVersionResponseProtobuf=new DaemonVersionResponseProtobuf{
                Type=2200,MajorVersion=version.Major,MinorVersion=version.Minor,BuildVersion=version.Build,RevisionVersion=version.Revision};
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnDaemonVersion","已处理 daemon-version 指令");
            return daemonVersionResponseProtobuf.ToByteArray();
        }
        /// <summary>
        /// windctl daemon-status
        /// </summary>
        /// <returns></returns>
        private static Byte[] OnDaemonStatus(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnDaemonStatus","开始处理 daemon-status 指令");
            ProcessProtobuf processProtobuf=new ProcessProtobuf{Id=Program.AppProcess.Id,StartTime=Program.AppProcess.StartTime.ToLocalTimestamp()};
            UnitPerformanceCounterProtobuf unitPerformanceCounter=new UnitPerformanceCounterProtobuf{RAM=0F,CPU=0F};
            UnitNetworkCounterProtobuf unitNetworkCounter =new UnitNetworkCounterProtobuf { SendSpeed=0,ReceiveSpeed=0,TotalSent=0,TotalReceived=0};
            if(Program.UnitPerformanceCounterModule.Useable) {
                unitPerformanceCounter.CPU=Program.UnitPerformanceCounterModule.GetCpuValue(Program.AppProcess.Id);
                unitPerformanceCounter.RAM=Program.UnitPerformanceCounterModule.GetRamValue(Program.AppProcess.Id);
            }
            if(Program.UnitNetworkCounterModule.Useable) {
                UnitNetworkCounter counter =Program.UnitNetworkCounterModule.GetValue(Program.AppProcess.Id);
                unitNetworkCounter.SendSpeed=counter.SendSpeed;
                unitNetworkCounter.ReceiveSpeed=counter.ReceiveSpeed;
                unitNetworkCounter.TotalSent=counter.TotalSent;
                unitNetworkCounter.TotalReceived=counter.TotalReceived;
            }
            DaemonProtobuf daemonProtobuf=new DaemonProtobuf{
                Name="wind",Description="a systemd for windows",
                ProcessProtobuf=processProtobuf,PerformanceCounterProtobuf=unitPerformanceCounter,NetworkCounterProtobuf=unitNetworkCounter,
                AbsoluteExecutePath=Program.AppEnvironment.BaseDirectory+"wind.exe",AbsoluteWorkDirectory=Program.AppEnvironment.BaseDirectory};
            DaemonStatusResponseProtobuf daemonStatusResponseProtobuf=new DaemonStatusResponseProtobuf{Type=2201,DaemonProtobuf=daemonProtobuf};
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnDaemonStatus","已处理 daemon-status 指令");
            return daemonStatusResponseProtobuf.ToByteArray();
        }
        /// <summary>
        /// windctl daemon-shutdown
        /// </summary>
        /// <returns></returns>
        private static Byte[] OnDaemonShutdown(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnDaemonShutdown","开始处理 daemon-shutdown 指令");
            DaemonShutdownResponseProtobuf daemonShutdownResponseProtobuf=new DaemonShutdownResponseProtobuf{Type=2299};
            Task.Run(()=>{
                SpinWait.SpinUntil(()=>false,1000);
                Program.DaemonServiceController.Stop();
                SpinWait.SpinUntil(()=>false,3000);
                Environment.Exit(0);
            });
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnDaemonShutdown","已处理 daemon-shutdown 指令");
            return daemonShutdownResponseProtobuf.ToByteArray();
        }
    }
}
