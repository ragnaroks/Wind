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

        /// <summary>单元存放目录</summary>
        private String UnitsDirectory{get;set;}=null;
        /// <summary>单元字典</summary>
        private Dictionary<String,Unit> UnitDictionary{get;set;}=new Dictionary<String,Unit>();
        
        #region IDisposable
        private bool disposedValue;
        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: 释放托管状态(托管对象)
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
        /// 加入单元
        /// </summary>
        /// <param name="unitKey"></param>
        /// <param name="unitSettings"></param>
        private void AddUnit(String unitKey,UnitSettings unitSettings){
            if(!this.Useable){return;}
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.AddUnit",$"正在增加\"{unitKey}\"单元");
            //检查是新增或更新
            if(this.UnitDictionary.ContainsKey(unitKey)) {
                this.UnitDictionary[unitKey].Settings=unitSettings;
                this.UnitDictionary[unitKey].SettingsUpdated=true;
            } else {
                this.UnitDictionary.Add(unitKey,new Unit{Key=unitKey,Settings=unitSettings});
            }
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.AddUnit",$"已增加\"{unitKey}\"单元");
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
            }).ConfigureAwait(false);
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
            unit.SettingsUpdated=false;
            ProcessStartInfo processStartInfo=new ProcessStartInfo{
                UseShellExecute=false,FileName=unit.RunningSettings.AbsoluteExecutePath,WorkingDirectory=unit.RunningSettings.AbsoluteWorkDirectory,
                CreateNoWindow=true,WindowStyle=ProcessWindowStyle.Hidden,Arguments=unit.RunningSettings.Arguments};
            unit.Process=new Process{StartInfo=processStartInfo,EnableRaisingEvents=true};
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
                unit.State=2;
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
        /// 解析所有单元配置文件
        /// </summary>
        /// <returns>解析成功数量</returns>
        public Int32 ParseAllUnits(){
            if(!this.Useable){return 0;}
            //读取文件目录
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.ParseAllUnits","开始解析所有单元配置文件");
            FileInfo[] fileInfoArray;
            try {
                DirectoryInfo directoryInfo=new DirectoryInfo(this.UnitsDirectory);
                fileInfoArray=directoryInfo.GetFiles("*.json",SearchOption.TopDirectoryOnly);
            }catch(Exception exception) {
                LoggerModuleHelper.TryLog(
                    "Modules.UnitManageModule.ParseAllUnits[Error]",
                    $"读取单元存放目录异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return 0;
            }
            if(fileInfoArray.Length<1){return 0;}
            //解析文件
            List<String> unitKeyList=new List<String>();
            for(Int32 i1=0;i1<fileInfoArray.Length;i1++){
                //key
                String unitKey=UnitManageModuleHelper.GetUnitKey(fileInfoArray[i1]);
                if(String.IsNullOrWhiteSpace(unitKey) || unitKey.Length>32){
                    LoggerModuleHelper.TryLog("Modules.UnitManageModule.ParseAllUnits[Warning]",$"单元文件\"{fileInfoArray[i1].FullName}\"标识错误,已跳过");
                    continue;
                }
                //setting
                UnitSettings unitSettings=UnitManageModuleHelper.ParseUnitSettingsFile(fileInfoArray[i1]);
                if(unitSettings==null) {
                    LoggerModuleHelper.TryLog("Modules.UnitManageModule.ParseAllUnits[Warning]",$"单元文件\"{fileInfoArray[i1].FullName}\"读取失败,已跳过");
                    continue;
                }
                //加入列表
                this.AddUnit(unitKey,unitSettings);
                LoggerModuleHelper.TryLog("Modules.UnitManageModule.ParseAllUnits",$"单元\"{unitKey}\"读取成功,已加入单元列表");
                //加入变更列表
                unitKeyList.Add(unitKey);
            }
            //停止已"消失"的单元
            foreach(KeyValuePair<String,Unit> item in this.UnitDictionary) {
                if(unitKeyList.Contains(item.Key)){continue;}
                //remove必定调用stop
                _=this.RemoveUnitAsync(item.Key);
            }
            //完成
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.ParseAllUnits",$"已解析{this.UnitDictionary.Count}个单元配置文件");
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
                /*_=Task.Run(async ()=>{
                    await this.StartUnitAsync(item.Key,onlyAutoStart).ConfigureAwait(false);
                });*/
                _=this.StartUnitAsync(item.Key,onlyAutoStart);
            }
        }

        /// <summary>
        /// 停止全部单元
        /// </summary>
        public void StopAllUnits(){
            if(!this.Useable){return;}
            if(this.UnitDictionary.Count<1){return;}
            foreach(KeyValuePair<String,Unit> item in this.UnitDictionary){
                /*_=Task.Run(async ()=>{
                    await this.StopUnitAsync(item.Key).ConfigureAwait(false);
                });*/
                _=this.StopUnitAsync(item.Key);
            }
        }

        /// <summary>
        /// 重启全部(正在运行的)单元
        /// </summary>
        public void RestartAllUnits() {
            if(!this.Useable){return;}
            if(this.UnitDictionary.Count<1){return;}
            foreach(KeyValuePair<String,Unit> item in this.UnitDictionary) {
                if(item.Value.State==0 || item.Value.State==3){continue;}
                /*_=Task.Run(async ()=>{
                    await this.StopUnitAsync(item.Key).ConfigureAwait(false);
                    await this.StartUnitAsync(item.Key,false).ConfigureAwait(false);
                });*/
                _=this.RestartUnitAsync(item.Key);
            }
        }

        /// <summary>
        /// 移除全部单元
        /// </summary>
        public void RemoveAllUnits() {
            if(!this.Useable){return;}
            if(this.UnitDictionary.Count<1){return;}
            foreach(KeyValuePair<String,Unit> item in this.UnitDictionary){
                /*_=Task.Run(async ()=>{
                    await this.RemoveUnitAsync(item.Key).ConfigureAwait(false);
                });*/
                _=this.RemoveUnitAsync(item.Key);
            }
        }
    }
}
