using wind.Entities.Common;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Session;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wind.Modules {
    public class UnitNetworkCounterModule:IDisposable {
        public Boolean Useable{get;private set;}=false;

        /// <summary>计数器字典,key是processId</summary>
        private ConcurrentDictionary<Int32,UnitNetworkCounter> UnitNetworkCounterDictionary{get;set;}=new ConcurrentDictionary<Int32,UnitNetworkCounter>();
        /// <summary>ETW会话</summary>
        private TraceEventSession TraceEventSession{get;set;}=null;
        /// <summary>异步控制句柄</summary>
        private CancellationTokenSource CancellationTokenSource{get;set;}=null;
        
        #region IDisposable
        private bool disposedValue;

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: 释放托管状态(托管对象)
                    this.TraceEventSession.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                this.UnitNetworkCounterDictionary=null;

                disposedValue=true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        ~UnitNetworkCounterModule(){
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: false);
        }

        public void Dispose() {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public Boolean Setup() {
            if(this.Useable){return true;}
            //初始化ETW会话
            try {
                this.TraceEventSession=new TraceEventSession(Program.AppEnvironment.UnitsNetworkTraceEventSession,TraceEventSessionOptions.Create){StopOnDispose=true};
            }catch(Exception exception) {
                Helpers.LoggerModuleHelper.TryLog(
                    "Modules.UnitNetworkCounterModule.Setup[Error]",
                    $"创建ETW会话异常,{exception.Message}\n异常堆栈: {exception.StackTrace}");
                return false;
            }
            Boolean b1=false;
            try {
                b1=this.TraceEventSession.EnableKernelProvider(KernelTraceEventParser.Keywords.NetworkTCPIP);
            }catch(Exception exception) {
                Helpers.LoggerModuleHelper.TryLog(
                    "Modules.UnitNetworkCounterModule.Setup[Error]",
                    $"ETW会话EnableKernelProvider异常,{exception.Message}\n异常堆栈: {exception.StackTrace}");
                return false;
            }
            if(!b1) {
                this.TraceEventSession.Dispose();
                Helpers.LoggerModuleHelper.TryLog("Modules.UnitNetworkCounterModule.Setup[Error]","ETW会话EnableKernelProvider失败");
                return false;
            }
            if(this.TraceEventSession.Source==null){
                this.TraceEventSession.Dispose();
                return false;
            }
            this.TraceEventSession.Source.Kernel.TcpIpRecv+=this.KernelTcpIpRecv;
            this.TraceEventSession.Source.Kernel.TcpIpSend+=this.KernelTcpIpSend;
            //
            this.CancellationTokenSource=new CancellationTokenSource();
            //完成
            this.Useable=true;
            return true;
        }

        public Boolean StartTraceEventSession(){
            if(!this.Useable){return false;}
            //异步处理
            Boolean flag=true;
            Task.Run(()=>{
                if(!this.TraceEventSession.Source.Process()){
                    this.TraceEventSession.Source.Kernel.TcpIpSend-=this.KernelTcpIpSend;
                    this.TraceEventSession.Source.Kernel.TcpIpRecv-=this.KernelTcpIpRecv;
                    Helpers.LoggerModuleHelper.TryLog("Modules.UnitNetworkCounterModule.StartTraceEventSession[Error]","process失败");
                    flag=false;
                }
            },this.CancellationTokenSource.Token);
            SpinWait.SpinUntil(()=>false,1000);//如果process失败,flag为假且立刻退出,等待一秒确保结果
            if(!flag){return false;}
            //异步处理2
            Task.Run(()=>{
                while(!this.CancellationTokenSource.IsCancellationRequested){
                    foreach(KeyValuePair<Int32,Entities.Common.UnitNetworkCounter> item in this.UnitNetworkCounterDictionary){
                        item.Value.SendSpeed=item.Value.TotalSent-item.Value.LastCulTotalSent;
                        item.Value.ReceiveSpeed=item.Value.TotalReceived-item.Value.LastCulTotalReceived;
                        item.Value.LastCulTotalReceived=item.Value.TotalReceived;
                        item.Value.LastCulTotalSent=item.Value.TotalSent;
                    }
                    SpinWait.SpinUntil(()=>false,1000);
                }
            },this.CancellationTokenSource.Token);
            //
            return true;
        }

        public Boolean StopTraceEventSession() {
            if(!this.Useable){return true;}
            if(this.TraceEventSession.Source!=null) {
                this.TraceEventSession.Source.Kernel.TcpIpSend-=this.KernelTcpIpSend;
                this.TraceEventSession.Source.Kernel.TcpIpRecv-=this.KernelTcpIpRecv;
            }
            this.CancellationTokenSource.Cancel();
            return true;
        }

        public Boolean Add(Int32 unitProcessId) {
            if(!this.Useable){return false;}
            if(this.UnitNetworkCounterDictionary.ContainsKey(unitProcessId)){return false;}
            return this.UnitNetworkCounterDictionary.TryAdd(unitProcessId,new UnitNetworkCounter());
        }

        public Boolean Remove(Int32 unitProcessId) {
            if(!this.Useable){return false;}
            if(!this.UnitNetworkCounterDictionary.ContainsKey(unitProcessId)){return false;}
            return this.UnitNetworkCounterDictionary.TryRemove(unitProcessId,out _);
        }

        public UnitNetworkCounter GetValue(Int32 unitProcessId){
            if(!this.Useable){return null;}
            if(!this.UnitNetworkCounterDictionary.ContainsKey(unitProcessId)){return null;}
            return this.UnitNetworkCounterDictionary[unitProcessId];
        }

        private void KernelTcpIpSend(Microsoft.Diagnostics.Tracing.Parsers.Kernel.TcpIpSendTraceData tcpIpSendTraceData) {
            if(!this.UnitNetworkCounterDictionary.ContainsKey(tcpIpSendTraceData.ProcessID)){return;}
            this.UnitNetworkCounterDictionary[tcpIpSendTraceData.ProcessID].TotalSent+=tcpIpSendTraceData.size;
        }

        private void KernelTcpIpRecv(Microsoft.Diagnostics.Tracing.Parsers.Kernel.TcpIpTraceData tcpIpTraceData) {
            if(!this.UnitNetworkCounterDictionary.ContainsKey(tcpIpTraceData.ProcessID)){return;}
            this.UnitNetworkCounterDictionary[tcpIpTraceData.ProcessID].TotalReceived+=tcpIpTraceData.size;
        }
    }
}
