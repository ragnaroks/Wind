using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace Daemon.Modules {
    /// <summary>应用程序性能计数器模块</summary>
    [Obsolete("可拓展至所有单元,就像网络统计模块一样")]
    public class AppPerformanceCounterModule:IDisposable {
        /// <summary>模块是否可用</summary>
        public Boolean ModuleAvailable{get;}=true;
        /// <summary>CPU时间计数器</summary>
        private PerformanceCounter ProcessorTimePerformanceCounter{get;}
        /// <summary>内存占用计数器</summary>
        private PerformanceCounter WorkingSetPerformanceCounter{get;}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design","CA1031:不捕获常规异常类型",Justification = "<挂起>")]
        public AppPerformanceCounterModule(){
            try {
                this.ProcessorTimePerformanceCounter=new PerformanceCounter{
                    CategoryName="Process",CounterName="% Processor Time",InstanceName=Program.AppProcess.ProcessName,ReadOnly=true};
            }catch(Exception exception) {
                Console.WriteLine(
                    $"Modules.AppPerformanceCounterModule.AppPerformanceCounterModule[Error] => 创建ProcessorTimePerformanceCounter时异常,{exception.Message},{exception.StackTrace}");
            }
            try {
                this.WorkingSetPerformanceCounter=new PerformanceCounter{
                    CategoryName="Process",CounterName="Working Set",InstanceName=Program.AppProcess.ProcessName,ReadOnly=true};
            }catch(Exception exception) {
                Console.WriteLine(
                    $"Modules.AppPerformanceCounterModule.AppPerformanceCounterModule[Error] => 创建ProcessorTimePerformanceCounter时异常,{exception.Message},{exception.StackTrace}");
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
            GC.SuppressFinalize(this);
        }
        #endregion

        public Single GetProcessTimePercentage()=>this.ProcessorTimePerformanceCounter==null?0F:this.ProcessorTimePerformanceCounter.NextValue();
        public Single GetProcessWorkingSetSize()=>this.WorkingSetPerformanceCounter==null?0F:this.WorkingSetPerformanceCounter.NextValue();
    }
}
