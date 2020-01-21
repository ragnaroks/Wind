using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Microsoft.Diagnostics.Tracing.Session;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Daemon.Modules {
    public class UnitNetworkPerformanceTracerModule:IDisposable{
        private TraceEventSession TraceEventSession;
        private Timer Timer;
        private Boolean TimerEnable=false;
        /*private Int64 UnitsTotalReceived=0;
        private Int64 UnitsTotalSent=0;
        private Int64 UnitsReceiveSpeed=0;
        private Int64 UnitsSendSpeed=0;*/
        private Dictionary<Int32,Entities.UnitNetworkCounter> CounterDictionary=new Dictionary<Int32,Entities.UnitNetworkCounter>();

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

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~UnitNetworkPerformanceTracerModule()
        // {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose() {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion

        public async Task<Boolean> Start(){
            try {
                this.TraceEventSession=new TraceEventSession($"Wind2UnitsNetworkTraceEventSession",TraceEventSessionOptions.Create){StopOnDispose=true};
            }catch(Exception exception) {
                Console.WriteLine($"Modules.UnitNetworkPerformanceTracerModule.UnitNetworkPerformanceTracerModule => new TraceEventSession时异常,{exception.Message},{exception.StackTrace}");
                Program.LoggerModule.Log("Modules.UnitNetworkPerformanceTracerModule.UnitNetworkPerformanceTracerModule[Error]",$"new TraceEventSession时异常,{exception.Message},{exception.StackTrace}");
                return false;
            }
            if(!this.TraceEventSession.EnableKernelProvider(KernelTraceEventParser.Keywords.NetworkTCPIP)) {
                this.TraceEventSession.Dispose();
                Console.WriteLine($"Modules.UnitNetworkPerformanceTracerModule.UnitNetworkPerformanceTracerModule => EnableKernelProvider失败");
                Program.LoggerModule.Log("Modules.UnitNetworkPerformanceTracerModule.UnitNetworkPerformanceTracerModule[Error]","EnableKernelProvider失败");
                return false;
            }
            this.CounterDictionary.Add(Program.AppProcess.Id,new Entities.UnitNetworkCounter());
            try {
                this.Timer=new Timer(this.TimerCallack,null,0,1000);
                this.TimerEnable=true;
            }catch(Exception exception) {
                Console.WriteLine($"Modules.UnitNetworkPerformanceTracerModule.UnitNetworkPerformanceTracerModule => new Timer时异常,{exception.Message},{exception.StackTrace}");
                Program.LoggerModule.Log("Modules.UnitNetworkPerformanceTracerModule.UnitNetworkPerformanceTracerModule[Error]",$"new Timer时异常,{exception.Message},{exception.StackTrace}");
                return false;
            }
            return await Task<Boolean>.Run(()=>{
                this.TraceEventSession.Source.Kernel.TcpIpRecv+=this.TraceEventSession_Kernel_TcpIpRecv;
                this.TraceEventSession.Source.Kernel.TcpIpSend+=this.TraceEventSession_Kernel_TcpIpSend;
                if(!this.TraceEventSession.Source.Process()) {
                    this.TraceEventSession.Source.Kernel.TcpIpRecv-=this.TraceEventSession_Kernel_TcpIpRecv;
                    this.TraceEventSession.Source.Kernel.TcpIpSend-=this.TraceEventSession_Kernel_TcpIpSend;
                    this.TraceEventSession.Dispose();
                    Console.WriteLine("Classes.NetworkPerformanceReporter.NetworkPerformanceReporter => TraceEventSession Source Process失败");
                    return false;
                }
                return true;
            });
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
