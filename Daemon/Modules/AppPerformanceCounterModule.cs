using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace Daemon.Modules {
    public class AppPerformanceCounterModule:IDisposable {
        private readonly PerformanceCounter ProcessorTimePerformanceCounter=null;
        private readonly Boolean ProcessorTimePerformanceCounterUseable=false;
        private readonly PerformanceCounter WorkingSetPerformanceCounter=null;
        private readonly Boolean WorkingSetPerformanceCounterUseable=false;
        
        public AppPerformanceCounterModule(){
            try {
                this.ProcessorTimePerformanceCounter=new PerformanceCounter{CategoryName="Process",CounterName="% Processor Time",InstanceName=Program.AppProcess.ProcessName,ReadOnly=true};
                this.ProcessorTimePerformanceCounterUseable=true;
            }catch(Exception exception) {
                Console.WriteLine($"Modules.AppPerformanceCounterModule.AppPerformanceCounterModule => 创建ProcessorTimePerformanceCounter性能计数器时异常,{exception.Message},{exception.StackTrace}");
            }
            try {
                this.WorkingSetPerformanceCounter=new PerformanceCounter{CategoryName="Process",CounterName="Working Set",InstanceName=Program.AppProcess.ProcessName,ReadOnly=true};
                this.WorkingSetPerformanceCounterUseable=true;
            }catch(Exception exception) {
                Console.WriteLine($"Modules.AppPerformanceCounterModule.AppPerformanceCounterModule => 创建ProcessorTimePerformanceCounter性能计数器时异常,{exception.Message},{exception.StackTrace}");
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: 释放托管状态(托管对象)。
                    this.ProcessorTimePerformanceCounter.Dispose();
                    this.WorkingSetPerformanceCounter.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~AppPerformanceCounterModule()
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

        public Single GetProcessTimePercentage(){
            if(!this.ProcessorTimePerformanceCounterUseable){return 0F;}
            return this.ProcessorTimePerformanceCounter.NextValue();
        }

        public Single GetProcessWorkingSetSize() {
            if(!this.WorkingSetPerformanceCounterUseable){return 0F;}
            return this.WorkingSetPerformanceCounter.NextValue();
        }
    }
}
