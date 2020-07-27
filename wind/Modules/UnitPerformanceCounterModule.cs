using wind.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace wind.Modules {
    /*
     * 同名进程会附加 #1,#2 之类的标识
     * 且其中一个同名进程退出后,所有同名进程的 #? 都会发生改变
     */

    /// <summary>性能计数器模块</summary>
    public class UnitPerformanceCounterModule:IDisposable {
        public Boolean Useable{get;private set;}=false;
        
        /// <summary>CPU性能计数器字典</summary>
        private ConcurrentDictionary<Int32,PerformanceCounter> CpuPerformanceCounterDictionary{get;set;}=new ConcurrentDictionary<Int32,PerformanceCounter>();
        /// <summary>RAM性能计数器字典</summary>
        private ConcurrentDictionary<Int32,PerformanceCounter> RamPerformanceCounterDictionary{get;set;}=new ConcurrentDictionary<Int32,PerformanceCounter>();
        
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
                this.CpuPerformanceCounterDictionary=null;
                this.RamPerformanceCounterDictionary=null;

                disposedValue=true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        ~UnitPerformanceCounterModule(){
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
            if(this.Useable){return true;}
            this.Useable=true;
            return true;
        }

        private void RemoveAll() {
            foreach(var item in this.CpuPerformanceCounterDictionary){item.Value.Dispose(); }
            this.CpuPerformanceCounterDictionary.Clear();
            foreach(var item in this.RamPerformanceCounterDictionary){item.Value.Dispose(); }
            this.RamPerformanceCounterDictionary.Clear();
        }

        public void Add(Int32 processId){
            if(!this.Useable){return;}
            if(this.CpuPerformanceCounterDictionary.ContainsKey(processId) || this.RamPerformanceCounterDictionary.ContainsKey(processId)){ this.Remove(processId); }
            String instanceName=UnitPerformanceCounterModuleHelper.GetActuallyInstanceNameByProcessId(processId);
            if(String.IsNullOrWhiteSpace(instanceName)){return;}
            try {
                PerformanceCounter performanceCounter=new PerformanceCounter{CategoryName="Process",CounterName="% Processor Time",InstanceName=instanceName,ReadOnly=true};
                _=performanceCounter.NextValue();//预热
                if(!this.CpuPerformanceCounterDictionary.TryAdd(processId,performanceCounter)) {
                    Helpers.LoggerModuleHelper.TryLog("Modules.UnitPerformanceCounterModule.Add[Error]",$"创建CPU性能计数器成功但加入列表失败");
                }
            }catch(Exception exception){
                Helpers.LoggerModuleHelper.TryLog(
                    "Modules.UnitPerformanceCounterModule.Add[Error]",
                    $"创建CPU性能计数器异常,{exception.Message}\n异常堆栈: {exception.StackTrace}");
            }
            
            try {
                PerformanceCounter performanceCounter=new PerformanceCounter{CategoryName="Process",CounterName="Working Set",InstanceName=instanceName,ReadOnly=true};
                _=performanceCounter.NextValue();//预热
                if(!this.RamPerformanceCounterDictionary.TryAdd(processId,performanceCounter)) {
                    Helpers.LoggerModuleHelper.TryLog("Modules.UnitPerformanceCounterModule.Add[Error]",$"创建RAM性能计数器成功但加入列表失败");
                }
            }catch(Exception exception) {
                LoggerModuleHelper.TryLog(
                    "Modules.UnitPerformanceCounterModule.Add[Error]",$"创建RAM性能计数器异常,{exception.Message}\n异常堆栈: {exception.StackTrace}");
            }
        }

        public Boolean Remove(Int32 processId) {
            if(!this.Useable){return false;}
            Boolean b1=false;
            if(this.CpuPerformanceCounterDictionary.Count>0 && this.CpuPerformanceCounterDictionary.ContainsKey(processId)){
                this.CpuPerformanceCounterDictionary[processId].Dispose();
                b1=this.CpuPerformanceCounterDictionary.TryRemove(processId,out _);
            }
            Boolean b2=false;
            if(this.RamPerformanceCounterDictionary.Count>0 && this.RamPerformanceCounterDictionary.ContainsKey(processId)){
                this.RamPerformanceCounterDictionary[processId].Dispose();
                b2=this.RamPerformanceCounterDictionary.TryRemove(processId,out _);
            }
            return b1 && b2;
        }

        public Single GetCpuValue(Int32 processId){
            if(!this.Useable){return 0F;}
            if(this.CpuPerformanceCounterDictionary.Count<1 || !this.CpuPerformanceCounterDictionary.ContainsKey(processId)){return 0F;}
            Single value;
            try{
                value=this.CpuPerformanceCounterDictionary[processId].NextValue();
            }catch(Exception exception) {
                LoggerModuleHelper.TryLog(
                    "Modules.UnitPerformanceCounterModule.GetCpuValue[Error]",$"读取CPU性能计数器异常,{exception.Message}\n异常堆栈: {exception.StackTrace}");
                value=this.GetCpuValueEx(processId);
            }
            return value;
        }

        public Single GetCpuValueEx(Int32 processId){
            if(!this.Useable){return 0F;}
            if(this.CpuPerformanceCounterDictionary.Count<1 || !this.CpuPerformanceCounterDictionary.ContainsKey(processId)){return 0F;}
            this.Remove(processId);
            this.Add(processId);
            Single value=0F;
            try{
                value=this.CpuPerformanceCounterDictionary[processId].NextValue();
            }catch(Exception exception) {
                LoggerModuleHelper.TryLog(
                    "Modules.UnitPerformanceCounterModule.GetCpuValueEx[Error]",$"读取CPU性能计数器异常,{exception.Message}\n异常堆栈: {exception.StackTrace}");
            }
            return value;
        }

        public Single GetRamValue(Int32 processId){
            if(!this.Useable){return 0F;}
            if(this.RamPerformanceCounterDictionary.Count<1 || !this.RamPerformanceCounterDictionary.ContainsKey(processId)){return 0F;}
            Single value;
            try{
                value=this.RamPerformanceCounterDictionary[processId].NextValue();
            }catch(Exception exception) {
                LoggerModuleHelper.TryLog(
                    "Modules.UnitPerformanceCounterModule.GetRamValue[Error]",$"读取RAM性能计数器异常,{exception.Message}\n异常堆栈: {exception.StackTrace}");
                value=this.GetRamValueEx(processId);
            }
            return value;
        }

        public Single GetRamValueEx(Int32 processId){
            if(!this.Useable){return 0F;}
            if(this.RamPerformanceCounterDictionary.Count<1 || !this.RamPerformanceCounterDictionary.ContainsKey(processId)){return 0F;}
            this.Remove(processId);
            this.Add(processId);
            Single value=0F;
            try{
                value=this.RamPerformanceCounterDictionary[processId].NextValue();
            }catch(Exception exception) {
                LoggerModuleHelper.TryLog(
                    "Modules.UnitPerformanceCounterModule.GetRamValueEx[Error]",$"读取RAM性能计数器异常,{exception.Message}\n异常堆栈: {exception.StackTrace}");
            }
            return value;
        }
    }
}
