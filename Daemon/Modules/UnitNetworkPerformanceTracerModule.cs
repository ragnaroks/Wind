using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Microsoft.Diagnostics.Tracing.Session;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Daemon.Modules {
    /// <summary>单元网络统计模块</summary>
    public class UnitNetworkPerformanceTracerModule:IDisposable{
        /// <summary>模块是否可用</summary>
        public Boolean ModuleAvailable{get;}=false;
        private TraceEventSession TraceEventSession{get;}
        private Timer Timer{get;}
        private Boolean TimerEnable{get;set;}=false;
        private Dictionary<Int32,Entities.UnitNetworkCounter> CounterDictionary{get;set;}=new Dictionary<int, Entities.UnitNetworkCounter>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design","CA1031:不捕获常规异常类型",Justification = "<挂起>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("样式","IDE0028:简化集合初始化",Justification = "<挂起>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization","CA1303:请不要将文本作为本地化参数传递",Justification = "<挂起>")]
        public UnitNetworkPerformanceTracerModule() {
            const String errorLogPrefix="Modules.UnitNetworkPerformanceTracerModule.UnitNetworkPerformanceTracerModule[Error]";
            this.CounterDictionary.Add(Program.AppProcess.Id,new Entities.UnitNetworkCounter());
            try {
                this.TraceEventSession=new TraceEventSession($"Wind2UnitsNetworkTraceEventSession",TraceEventSessionOptions.Create){StopOnDispose=true};
            }catch(Exception exception) {
                Console.WriteLine($"{errorLogPrefix} =>初始化TraceEventSession时异常,{exception.Message},{exception.StackTrace}");
                Program.LoggerModule.Log(errorLogPrefix,$"初始化TraceEventSession时异常,{exception.Message},{exception.StackTrace}");
                throw;
            }
            if(!this.TraceEventSession.EnableKernelProvider(KernelTraceEventParser.Keywords.NetworkTCPIP)) {
                this.TraceEventSession.Dispose();
                Console.WriteLine($"{errorLogPrefix} => EnableKernelProvider失败");
                Program.LoggerModule.Log(errorLogPrefix,"EnableKernelProvider失败");
                return;
            }
            try {
                this.Timer=new Timer(this.TimerCallack,null,0,1000);
            }catch(Exception exception) {
                Console.WriteLine($"{errorLogPrefix} => 初始化Timer时异常,{exception.Message},{exception.StackTrace}");
                Program.LoggerModule.Log(errorLogPrefix,$"初始化Timer时异常,{exception.Message},{exception.StackTrace}");
                throw;
            }
            this.ModuleAvailable=true;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: 释放托管状态(托管对象)。
                    this.Timer.Dispose();
                    this.TraceEventSession.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                this.CounterDictionary=null;

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~UnitNetworkPerformanceTracerModule(){
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(false);
        }

        // 添加此代码以正确实现可处置模式。
        public void Dispose() {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization","CA1303:请不要将文本作为本地化参数传递",Justification = "<挂起>")]
        public Boolean StartProcess() {
            const String infoLogPrefix="Modules.UnitNetworkPerformanceTracerModule.StartProcess";
            this.TraceEventSession.Source.Kernel.TcpIpRecv+=this.TraceEventSession_Kernel_TcpIpRecv;
            this.TraceEventSession.Source.Kernel.TcpIpSend+=this.TraceEventSession_Kernel_TcpIpSend;
            this.TimerEnable=true;
            if(!this.TraceEventSession.Source.Process()) {
                this.TraceEventSession.Source.Kernel.TcpIpRecv-=this.TraceEventSession_Kernel_TcpIpRecv;
                this.TraceEventSession.Source.Kernel.TcpIpSend-=this.TraceEventSession_Kernel_TcpIpSend;
                this.TraceEventSession.Dispose();
                Console.WriteLine($"{infoLogPrefix} => TraceEventSession Source Process失败");
                return false;
                this.TimerEnable=false;
            }
            return true;
        }

        private void TraceEventSession_Kernel_TcpIpSend(TcpIpSendTraceData tcpIpSendTraceData) {
            if(!this.CounterDictionary.ContainsKey(tcpIpSendTraceData.ProcessID)){return;}
            this.CounterDictionary[tcpIpSendTraceData.ProcessID].TotalSent+=tcpIpSendTraceData.size;
        }

        private void TraceEventSession_Kernel_TcpIpRecv(TcpIpTraceData tcpIpTraceData) {
            if(!this.CounterDictionary.ContainsKey(tcpIpTraceData.ProcessID)){return;}
            this.CounterDictionary[tcpIpTraceData.ProcessID].TotalReceived+=tcpIpTraceData.size;
        }

        private void TimerCallack(Object state) {
            if(!this.TimerEnable || this.CounterDictionary.Count<1){return;}
            this.TimerEnable=false;
            foreach(KeyValuePair<Int32,Entities.UnitNetworkCounter> item in this.CounterDictionary){
                item.Value.SendSpeed=item.Value.TotalSent-item.Value.LastCulTotalSent;
                item.Value.ReceiveSpeed=item.Value.TotalReceived-item.Value.LastCulTotalReceived;
                item.Value.LastCulTotalReceived=item.Value.TotalReceived;
                item.Value.LastCulTotalSent=item.Value.TotalSent;
            }
            this.TimerEnable=true;
        }

        public Entities.UnitNetworkCounter GetCounter(Int32 processId){
            if(this.CounterDictionary.Count<1 || !this.CounterDictionary.ContainsKey(processId)){return null;}
            return this.CounterDictionary[processId];
        }

        public void AddCounter(Int32 processId) {
            if(this.CounterDictionary.ContainsKey(processId)){return;}
            this.CounterDictionary.Add(processId,new Entities.UnitNetworkCounter());
        }

        public void RemoveCounter(Int32 processId) {
            if(!this.CounterDictionary.ContainsKey(processId)){return;}
            this.CounterDictionary.Remove(processId);
        }
    }
}
