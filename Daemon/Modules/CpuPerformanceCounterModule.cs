using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Daemon.Modules {
    public class CpuPerformanceCounterModule:IDisposable {
        public Boolean Useable{get;private set;}=false;

        /// <summary>性能计数器字典</summary>
        private Dictionary<String,PerformanceCounter> PerformanceCounterDictionary{get;set;}=new Dictionary<String,PerformanceCounter>();

        #region IDisposable
        private bool disposedValue;

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: 释放托管状态(托管对象)
                    this.RemoveAll();
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                this.PerformanceCounterDictionary=null;

                disposedValue=true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        ~CpuPerformanceCounterModule(){
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: false);
        }

        public void Dispose() {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public Boolean Setup(){
            this.Useable=true;
            return true;
        }

        private void RemoveAll() {
            foreach(var item in this.PerformanceCounterDictionary){ item.Value.Dispose(); }
        }

        public void Add(String unitKey,String processName){
            if(!this.Useable){return;}
            if(this.PerformanceCounterDictionary.ContainsKey(unitKey)){ this.PerformanceCounterDictionary[unitKey].Dispose(); }
            this.PerformanceCounterDictionary[unitKey]=new PerformanceCounter{CategoryName="Process",CounterName="% Processor Time",InstanceName=processName,ReadOnly=true};
        }

        public void Remove(String unitKey) {
            if(!this.Useable){return;}
            if(this.PerformanceCounterDictionary.Count<1 || !this.PerformanceCounterDictionary.ContainsKey(unitKey)){return;}
            this.PerformanceCounterDictionary[unitKey].Dispose();
            this.PerformanceCounterDictionary.Remove(unitKey);
        }

        public Single GetValue(String unitKey){
            if(!this.Useable){return 0F;}
            if(this.PerformanceCounterDictionary.Count<1 || !this.PerformanceCounterDictionary.ContainsKey(unitKey)){return 0F;}
            return this.PerformanceCounterDictionary[unitKey].NextValue();
        }
    }
}
