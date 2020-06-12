using Daemon.Entities.Common;
using Daemon.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Daemon.Modules {
    /// <summary>单元管理模块</summary>
    public class UnitManageModule:IDisposable{
        public Boolean Useable{get;private set;}=false;

        /// <summary>单元存放目录,无路径分隔符</summary>
        private String UnitsDirectory{get;set;}=null;
        /// <summary>单元字典</summary>
        private Dictionary<String,Unit> UnitDictionary{get;set;}=new Dictionary<String,Unit>();
        
        #region IDisposable
        private bool disposedValue;

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: 释放托管状态(托管对象)
                    this.StopAllUnits();
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                disposedValue=true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~UnitManageModule()
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
        /// <param name="unitsDirectory"></param>
        /// <returns></returns>
        public Boolean Setup(String unitsDirectory){
            if(this.Useable){return true;}
            //检查单元存放路径,不要尝试创建
            if(!Directory.Exists(unitsDirectory)){return false;}
            this.UnitsDirectory=unitsDirectory;
            //完成
            this.Useable=true;
            return true;
        }

        /// <summary>
        /// 单元退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUnitProcessExited(object sender,EventArgs e) {
            if(this.UnitDictionary.Count<1){return;}
            Process exitedProcess=sender as Process;
            Int32 exitedProcessExitCode=exitedProcess.ExitCode;
            Int32 exitedProcessId=exitedProcess.Id;
            exitedProcess.Dispose();
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.OnUnitProcessExited",$"进程[{exitedProcessId}]主动退出,退出代码[{exitedProcessExitCode}]");
            Unit unit=null;
            foreach(KeyValuePair<String,Unit> item in this.UnitDictionary) {
                if(item.Value.ProcessId!=exitedProcessId){continue;}
                unit=item.Value;
                break;
            }
            //if(unit.Process!=null){ unit.Process.Dispose(); }
            if(Program.CpuPerformanceCounterModule.Useable){ Program.CpuPerformanceCounterModule.Remove(unit.Key); }
            if(Program.RamPerformanceCounterModule.Useable){ Program.RamPerformanceCounterModule.Remove(unit.Key); }
            //如果是单元停止,此时state==3,否则可能是1||2
            if(unit.State==3){return;}
            Program.LoggerModule.Log("Modules.UnitManageModule.OnUnitProcessExited",$"单元\"{unit.Key}\"异常退出");
            if(!unit.RunningSettings.RestartWhenException){return;}
            unit.State=0;
            _=this.StartUnitAsync(unit.Key,false);
            Program.LoggerModule.Log("Modules.UnitManageModule.OnUnitProcessExited",$"单元\"{unit.Key}\"已重新启动");
        }

        /// <summary>
        /// 获取单元
        /// </summary>
        /// <param name="unitKey"></param>
        /// <returns></returns>
        public Unit GetUnit(String unitKey){
            if(!Useable){return null;}
            if(this.UnitDictionary.Count<1 || !this.UnitDictionary.ContainsKey(unitKey)){return null;}
            return this.UnitDictionary[unitKey];
        }

        /// <summary>
        /// 停止单元
        /// </summary>
        /// <param name="unitKey"></param>
        public async Task StopUnitAsync(String unitKey){
            if(!this.Useable){return;}
            if(this.UnitDictionary.Count<1 || !this.UnitDictionary.ContainsKey(unitKey)){return;}
            Unit unit=this.UnitDictionary[unitKey];
            if(unit.State==0 || unit.State==3){return;}
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.StopUnitAsync",$"正在停止\"{unitKey}\"单元");
            unit.State=3;
            await Task.Run(()=>{
                if(unit.Process!=null){
                    try {
                        unit.Process.Kill(unit.RunningSettings.Type==1);
                        unit.Process.WaitForExit();
                    } catch(Exception exception) {
                        LoggerModuleHelper.TryLog("Modules.UnitManageModule.StopUnitAsync[Error]",$"停止\"{unitKey}\"单元异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                    } finally {
                        unit.Process.Dispose();
                    }
                }
                unit.RunningSettings=null;
                unit.Process=null;
                if(Program.CpuPerformanceCounterModule.Useable){ Program.CpuPerformanceCounterModule.Remove(unitKey); }
                if(Program.RamPerformanceCounterModule.Useable){ Program.RamPerformanceCounterModule.Remove(unitKey); }
            }).ConfigureAwait(false);
            unit.ProcessId=0;
            unit.State=0;
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.StopUnitAsync",$"已停止\"{unitKey}\"单元");
        }

        /// <summary>
        /// 启动单元
        /// </summary>
        /// <param name="unitKey"></param>
        /// <param name="forAutoStart"></param>
        public async Task StartUnitAsync(String unitKey,Boolean forAutoStart){
            if(!this.Useable){return;}
            if(this.UnitDictionary.Count<1 || !this.UnitDictionary.ContainsKey(unitKey)){return;}
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.StartUnitAsync",$"正在启动\"{unitKey}\"单元");
            Unit unit=this.UnitDictionary[unitKey];
            if(unit.State==1 || unit.State==2){return;}
            unit.State=1;
            await Task.Delay(unit.State==3?3000:1000).ConfigureAwait(false);
            unit.RunningSettings=unit.Settings.DeepClone();
            ProcessStartInfo processStartInfo=new ProcessStartInfo{
                UseShellExecute=false,FileName=unit.RunningSettings.AbsoluteExecutePath,WorkingDirectory=unit.RunningSettings.AbsoluteWorkDirectory,
                CreateNoWindow=true,WindowStyle=ProcessWindowStyle.Hidden,Arguments=unit.RunningSettings.Arguments};
            unit.Process=new Process{StartInfo=processStartInfo,EnableRaisingEvents=true};
            unit.Process.Exited+=this.OnUnitProcessExited;
            if(forAutoStart && unit.RunningSettings.AutoStartDelay>0){ await Task.Delay(unit.RunningSettings.AutoStartDelay*1000).ConfigureAwait(false); }
            Boolean b1=false;
            try {
                b1=unit.Process.Start();
            }catch(Exception exception) {
                LoggerModuleHelper.TryLog(
                    "Modules.UnitManageModule.StartUnitAsync[Error]",
                    $"启动\"{unitKey}\"单元异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
            }
            if(b1) {
                unit.ProcessId=unit.Process.Id;
                unit.State=2;
                if(Program.CpuPerformanceCounterModule.Useable){ Program.CpuPerformanceCounterModule.Add(unitKey,unit.Process.ProcessName); }
                if(Program.RamPerformanceCounterModule.Useable){ Program.RamPerformanceCounterModule.Add(unitKey,unit.Process.ProcessName); }
                LoggerModuleHelper.TryLog("Modules.UnitManageModule.StartUnitAsync",$"已启动\"{unitKey}\"单元");
            } else {
                unit.State=0;
                LoggerModuleHelper.TryLog("Modules.UnitManageModule.StartUnitAsync",$"启动\"{unitKey}\"单元失败");
            }
        }

        /// <summary>
        /// 重启单元
        /// </summary>
        /// <param name="unitKey"></param>
        /// <returns></returns>
        public async Task RestartUnitAsync(String unitKey) {
            if(!this.Useable){return;}
            if(this.UnitDictionary.Count<1 || !this.UnitDictionary.ContainsKey(unitKey)){return;}
            await this.StopUnitAsync(unitKey).ConfigureAwait(false);
            await this.StartUnitAsync(unitKey,false).ConfigureAwait(false);
        }

        /// <summary>
        /// 移除单元
        /// </summary>
        /// <param name="unitKey"></param>
        public async Task RemoveUnitAsync(String unitKey) {
            if(!this.Useable){return;}
            if(this.UnitDictionary.Count<1 || !this.UnitDictionary.ContainsKey(unitKey)){return;}
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.RemoveUnitAsync",$"正在移除\"{unitKey}\"单元");
            await this.StopUnitAsync(unitKey).ConfigureAwait(false);
            this.UnitDictionary.Remove(unitKey);
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.RemoveUnitAsync",$"已移除\"{unitKey}\"单元");
        }

        /// <summary>
        /// 加载单元
        /// </summary>
        /// <param name="unitKey"></param>
        /// <returns></returns>
        public Boolean LoadUnit(String unitKey){
            if(!this.Useable){return false;}
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.LoadUnit","开始加载单元配置文件");
            //读取文件
            String unitFilePath=String.Concat(this.UnitsDirectory,Path.DirectorySeparatorChar,unitKey,".json");
            if(!File.Exists(unitFilePath)){
                LoggerModuleHelper.TryLog("Modules.UnitManageModule.LoadUnit[Error]",$"单元配置文件 {unitFilePath} 不存在");
                return false;
            }
            FileInfo fileInfo;
            try {
                fileInfo=new FileInfo(unitFilePath);
            }catch(Exception exception) {
                LoggerModuleHelper.TryLog(
                    "Modules.UnitManageModule.LoadUnit[Error]",
                    $"单元配置文件 {unitFilePath} 不存在\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return false;
            }
            //setting
            UnitSettings unitSettings=UnitManageModuleHelper.ParseUnitSettingsFile(fileInfo);
            if(unitSettings==null){
                LoggerModuleHelper.TryLog("Modules.UnitManageModule.LoadUnit[Warning]",$"单元文件\"{unitFilePath}\"读取失败");
                return false;
            }
            //检查是新增或更新
            if(this.UnitDictionary.ContainsKey(unitKey)) {
                this.UnitDictionary[unitKey].Settings=unitSettings;
            } else {
                this.UnitDictionary.Add(unitKey,new Unit{Key=unitKey,Settings=unitSettings});
            }
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.LoadUnit",$"单元\"{unitKey}\"读取成功,已加入单元列表");
            return true;
        }

        /// <summary>
        /// 解析所有单元配置文件
        /// </summary>
        /// <returns>解析成功数量</returns>
        public Int32 LoadAllUnits(){
            if(!this.Useable){return 0;}
            //读取文件目录
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.LoadAllUnits","开始解析所有单元配置文件");
            FileInfo[] fileInfoArray;
            try {
                DirectoryInfo directoryInfo=new DirectoryInfo(this.UnitsDirectory);
                fileInfoArray=directoryInfo.GetFiles("*.json",SearchOption.TopDirectoryOnly);
            }catch(Exception exception) {
                LoggerModuleHelper.TryLog(
                    "Modules.UnitManageModule.LoadAllUnits[Error]",
                    $"读取单元存放目录异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return 0;
            }
            if(fileInfoArray.Length<1){return 0;}
            //解析文件
            for(Int32 i1=0;i1<fileInfoArray.Length;i1++){
                //key
                String unitKey=UnitManageModuleHelper.GetUnitKey(fileInfoArray[i1]);
                if(String.IsNullOrWhiteSpace(unitKey) || unitKey.Length>32){
                    LoggerModuleHelper.TryLog("Modules.UnitManageModule.LoadAllUnits[Warning]",$"单元文件\"{fileInfoArray[i1].FullName}\"标识错误,已跳过");
                    continue;
                }
                //setting
                UnitSettings unitSettings=UnitManageModuleHelper.ParseUnitSettingsFile(fileInfoArray[i1]);
                if(unitSettings==null) {
                    LoggerModuleHelper.TryLog("Modules.UnitManageModule.LoadAllUnits[Warning]",$"单元文件\"{fileInfoArray[i1].FullName}\"读取失败,已跳过");
                    continue;
                }
                //检查是新增或更新
                if(this.UnitDictionary.ContainsKey(unitKey)) {
                    this.UnitDictionary[unitKey].Settings=unitSettings;
                } else {
                    this.UnitDictionary.Add(unitKey,new Unit{Key=unitKey,Settings=unitSettings});
                }
                LoggerModuleHelper.TryLog("Modules.UnitManageModule.LoadAllUnits",$"单元\"{unitKey}\"读取成功,已加入单元列表");
            }
            //完成
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.LoadAllUnits",$"已解析{this.UnitDictionary.Count}个单元配置文件");
            return this.UnitDictionary.Count;
        }

        /// <summary>
        /// 启动全部单元
        /// </summary>
        /// <param name="onlyAutoStart">仅自启单元</param>
        public void StartAllUnits(Boolean onlyAutoStart){
            if(!this.Useable){return;}
            if(this.UnitDictionary.Count<1){return;}
            foreach(KeyValuePair<String,Unit> item in this.UnitDictionary) {
                if(onlyAutoStart && !item.Value.Settings.AutoStart){continue;}
                _=this.StartUnitAsync(item.Key,onlyAutoStart);
            }
        }

        /// <summary>
        /// 停止全部单元
        /// </summary>
        public void StopAllUnits(){
            if(!this.Useable){return;}
            if(this.UnitDictionary.Count<1){return;}
            foreach(KeyValuePair<String,Unit> item in this.UnitDictionary){ _=this.StopUnitAsync(item.Key); }
        }

        /// <summary>
        /// 重启全部(正在运行的)单元
        /// </summary>
        public void RestartAllUnits() {
            if(!this.Useable){return;}
            if(this.UnitDictionary.Count<1){return;}
            foreach(KeyValuePair<String,Unit> item in this.UnitDictionary) {
                if(item.Value.State==0 || item.Value.State==3){continue;}
                _=this.RestartUnitAsync(item.Key);
            }
        }

        /// <summary>
        /// 移除全部单元
        /// </summary>
        public void RemoveAllUnits() {
            if(!this.Useable){return;}
            if(this.UnitDictionary.Count<1){return;}
            foreach(KeyValuePair<String,Unit> item in this.UnitDictionary){ _=this.RemoveUnitAsync(item.Key); }
        }
    }
}
