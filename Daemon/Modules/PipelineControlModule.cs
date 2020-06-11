using Daemon.Entities.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Daemon.Modules {
    /// <summary>本地(命名管道)管理模块</summary>
    public class PipelineControlModule:IDisposable {
        public Boolean Useable{get;private set;}=false;

        /// <summary>命名管道</summary>
        private NamedPipeServerStream NamedPipeServerStream{get;set;}=null;
        /// <summary>命名管道是否启用</summary>
        private Boolean NamedPipeServerStreamEnable{get;set;}=false;
        private String PipelineName{get;set;}=null;

        #region IDisposable
        private bool disposedValue;

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: 释放托管状态(托管对象)
                    this.NamedPipeServerStreamEnable=false;
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
            //创建命名管道
            Task.Run(()=>{
                try {
                    while(this.NamedPipeServerStreamEnable) {
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
                    }
                }catch(Exception exception) {
                    Helpers.LoggerModuleHelper.TryLog(
                        "Modules.PipelineControlModule.StartServer[Error]",
                        $"命名管道异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                }
            });
            this.NamedPipeServerStreamEnable=true;
            //完成
            this.Useable=true;
            return true;
        }

        public void StartServer()=>this.NamedPipeServerStreamEnable=true;

        public void StopServer()=>this.NamedPipeServerStreamEnable=false;

        private Byte[] OnMessage(Byte[] bytes){
            if(!this.Useable){return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00};}
            switch(bytes[0]) {
                case 0x01:return OnMessage01(bytes);
                case 0xFF:return OnMessageFF();
                default:
                    Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessage","命名管道收到未知指令");
                    return new Byte[23]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x75,0x6E,0x6B,0x6E,0x6F,0x77,0x6E,0x20,0x63,0x6F,0x6D,0x6D,0x61,0x6E,0x64};
            }
        }

        private static Byte[] OnMessage01(Byte[] bytes){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessage01","开始处理 status 指令");
            //解析 unitKey
            String unitKey;
            try {
                unitKey=Encoding.UTF8.GetString(bytes,8,104-8).TrimEnd().TrimEnd(new Char[]{'\0'});
            }catch(Exception exception){
                Helpers.LoggerModuleHelper.TryLog(
                    "Modules.PipelineControlModule.OnMessage01[Error]",
                    $"解析 unitKey 异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            if(String.IsNullOrWhiteSpace(unitKey)){
                Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessage01[Error]","解析 unitKey 失败");
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
            unitStatusText.Append(unitKey).Append(" - ").Append(unit.RunningSettings.Description).Append('\n');
            unitStatusText.Append("Loaded: ")
                .Append(unit.SettingsUpdated?"unloaded":"loaded")
                .Append('(')
                .Append(Program.AppEnvironment.UnitsDirectory).Append(Path.DirectorySeparatorChar).Append(unitKey).Append(".json; ")
                .Append(unit.RunningSettings.AutoStart?"enabled":"disabled")
                .Append(")\n");
            unitStatusText.Append("State: ");
            switch(unit.State) {
                case 0:unitStatusText.Append("stopped");break;
                case 1:unitStatusText.Append("starting #").Append(unit.Process.Id);break;
                case 2:unitStatusText.Append("started #").Append(unit.Process.Id);break;
                case 3:unitStatusText.Append("stopping");break;
                default:unitStatusText.Append("unknown");break;
            }
            unitStatusText.Append('\n');
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessage01","已处理 status 指令");
            return new Byte[8]{0x01,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes(unitStatusText.ToString())).ToArray();
        }

        private static Byte[] OnMessageFF(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessage","开始处理 shutdown 指令");
            Task.Run(()=>{
                SpinWait.SpinUntil(()=>false,1500);
                Environment.Exit(0);
            });
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessage","已处理 shutdown 指令");
            return new Byte[8]{0xFF,0x00,0x00,0x00,0x00,0x00,0x00,0x00};
        }
    }
}
