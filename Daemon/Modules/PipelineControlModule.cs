using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
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
                        this.NamedPipeServerStream=new NamedPipeServerStream(this.PipelineName,PipeDirection.In,1,PipeTransmissionMode.Message,PipeOptions.Asynchronous|PipeOptions.WriteThrough);
                        this.NamedPipeServerStream.WaitForConnection();
                        Byte[] buffer=new Byte[100];//100字节应该够了,4字节头部,96字节字符串
                        this.NamedPipeServerStream.Read(buffer,0,100);
                        this.OnMessage(buffer);
                        this.NamedPipeServerStream.Dispose();
                    }
                }catch(Exception exception) {
                    Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.StartServer[Error]",$"命名管道异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                }
            });
            this.NamedPipeServerStreamEnable=true;
            //完成
            this.Useable=true;
            return true;
        }

        public void StartServer()=>this.NamedPipeServerStreamEnable=true;

        public void StopServer()=>this.NamedPipeServerStreamEnable=false;

        private void OnMessage(Byte[] bytes){
            if(bytes.GetLength(0)!=100){return;}
            switch(bytes[0]) {
                case 0x00:
                    Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessage",$"命名管道收到 statusUnit 的处理请求");
                    break;
                case 0x01:
                    Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessage",$"命名管道收到 parseAllUnits 的处理请求");
                    break;
                case 0x02:
                    String parseUnitKey=Encoding.UTF8.GetString(bytes,4,96);
                    Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessage",$"命名管道收到 parseUnit=>{parseUnitKey} 的处理请求");
                    break;
                case 0x03:
                    String startUnitKey=Encoding.UTF8.GetString(bytes,4,96);
                    Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessage",$"命名管道收到 startUnit=>{startUnitKey} 的处理请求");
                    break;
                case 0x04:
                    String stoptUnitKey=Encoding.UTF8.GetString(bytes,4,96);
                    Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessage",$"命名管道收到 stopUnit=>{stoptUnitKey} 的处理请求");
                    break;
                default:
                    Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessage",$"命名管道收到未知的处理请求");
                    break;
            }
        }
    }
}
