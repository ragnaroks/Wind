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
            ProcessStartInfo processStartInfo=new ProcessStartInfo{
                UseShellExecute=false,FileName=unitSettings.AbsoluteExecutePath,WorkingDirectory=unitSettings.AbsoluteWorkDirectory,CreateNoWindow=true,
                WindowStyle=ProcessWindowStyle.Hidden,Arguments=unitSettings.Arguments};
            Process process=new Process {StartInfo=processStartInfo,EnableRaisingEvents=true};
            //检查是新增或更新
            if(this.UnitDictionary.ContainsKey(unitKey)) {
                this.UnitDictionary[unitKey].Settings=unitSettings;
                this.UnitDictionary[unitKey].Process=process;
                this.UnitDictionary[unitKey].SettingsUpdated=true;
            } else {
                this.UnitDictionary.Add(unitKey,new Unit{Key=unitKey,Settings=unitSettings,Process=process});
            }
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.AddUnit",$"已增加\"{unitKey}\"单元");
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
                if(unit.RunningProcess!=null){
                    try {
                        unit.RunningProcess.Kill(true);
                        unit.RunningProcess.WaitForExit();
                    } catch(Exception exception) {
                        LoggerModuleHelper.TryLog("Modules.UnitManageModule.StopUnitAsync[Error]",$"停止\"{unitKey}\"单元异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                    } finally {
                        unit.RunningProcess.Dispose();
                    }
                }
                unit.RunningSettings=null;
                unit.RunningProcess=null;
            });
            unit.State=0;
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.StopUnitAsync",$"已停止\"{unitKey}\"单元");
        }

        /// <summary>
        /// 启动单元
        /// </summary>
        /// <param name="unitKey"></param>
        public async Task StartUnitAsync(String unitKey){
            if(!this.Useable){return;}
            if(this.UnitDictionary.Count<1 || !this.UnitDictionary.ContainsKey(unitKey)){return;}
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.StartUnitAsync",$"正在启动\"{unitKey}\"单元");
            Unit unit=this.UnitDictionary[unitKey];
            if(unit.State==1 || unit.State==2){return;}
            await Task.Delay(unit.State==3?3000:1000);
            unit.RunningSettings=unit.Settings.DeepClone();
            unit.RunningProcess=unit.RunningProcess.DeepClone();
            unit.SettingsUpdated=false;
            Boolean b1=false;
            try {
                b1=unit.RunningProcess.Start();
            }catch(Exception exception) {
                LoggerModuleHelper.TryLog("Modules.UnitManageModule.StartUnitAsync[Error]",$"启动\"{unitKey}\"单元异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
            }
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.StartUnitAsync",b1?$"已启动\"{unitKey}\"单元":$"启动\"{unitKey}\"单元失败");
        }

        /// <summary>
        /// 移除单元
        /// </summary>
        /// <param name="unitKey"></param>
        public async Task RemoveUnitAsync(String unitKey) {
            if(!this.Useable){return;}
            if(this.UnitDictionary.Count<1 || !this.UnitDictionary.ContainsKey(unitKey)){return;}
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.RemoveUnitAsync",$"正在移除\"{unitKey}\"单元");
            await this.StopUnitAsync(unitKey);
            this.UnitDictionary.Remove(unitKey);
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.RemoveUnitAsync",$"已移除\"{unitKey}\"单元");
        }

        /// <summary>
        /// 解析所有单元配置文件
        /// </summary>
        /// <returns>解析成功数量</returns>
        public async Task<Int32> ParseAllUnitsAsync(){
            if(!this.Useable){return 0;}
            //读取文件目录
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.ParseAllUnits","开始解析所有单元配置文件");
            FileInfo[] fileInfoArray;
            try {
                DirectoryInfo directoryInfo=new DirectoryInfo(this.UnitsDirectory);
                fileInfoArray=directoryInfo.GetFiles("*.json",SearchOption.TopDirectoryOnly);
            }catch(Exception exception) {
                LoggerModuleHelper.TryLog("Modules.UnitManageModule.ParseAllUnits[Error]",$"读取单元存放目录异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return 0;
            }
            if(fileInfoArray.Length<1){return 0;}
            //解析文件
            List<String> unitKeyList=new List<String>();
            for(Int32 i1=0;i1<fileInfoArray.Length;i1++){
                //key
                String unitKey=UnitManageModuleHelper.GetUnitKey(fileInfoArray[i1]);
                if(String.IsNullOrWhiteSpace(unitKey) || unitKey.Length>32){
                    LoggerModuleHelper.TryLog("Modules.UnitManageModule.ParseAllUnits[Warning]",$"单元\"{fileInfoArray[i1].FullName}\"标识错误,已跳过");
                    continue;
                }
                //setting
                UnitSettings unitSettings=UnitManageModuleHelper.ParseUnitSettingsFile(fileInfoArray[i1]);
                if(unitSettings==null) {
                    LoggerModuleHelper.TryLog("Modules.UnitManageModule.ParseAllUnits[Warning]",$"单元\"{fileInfoArray[i1].FullName}\"读取失败,已跳过");
                    continue;
                }
                //加入列表
                this.AddUnit(unitKey,unitSettings);
                LoggerModuleHelper.TryLog("Modules.UnitManageModule.ParseAllUnits",$"单元\"{fileInfoArray[i1].FullName}\"读取成功,已加入单元列表");
                //加入变更列表
                unitKeyList.Add(unitKey);
            }
            //停止已"消失"的单元
            foreach(KeyValuePair<String,Unit> item in this.UnitDictionary) {
                if(unitKeyList.Contains(item.Key)){continue;}
                await this.RemoveUnitAsync(item.Key);//remove必定调用stop
            }
            //完成
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.ParseAllUnits",$"已解析{this.UnitDictionary.Count}个单元配置文件");
            return this.UnitDictionary.Count;
        }

        /// <summary>
        /// 启动全部单元
        /// </summary>
        /// <param name="onlyAutoStart">仅自启单元</param>
        public async Task StartAllUnitsAsync(Boolean onlyAutoStart){
            if(!this.Useable){return;}
            if(this.UnitDictionary.Count<1){return;}
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.StartAllUnitsAsync",onlyAutoStart?"正在启动自启单元":"正在启动全部单元");
            foreach(KeyValuePair<String,Unit> item in this.UnitDictionary) {
                if(onlyAutoStart && !item.Value.Settings.AutoStart){continue;}
                await this.StartUnitAsync(item.Key);
            }
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.StartAllUnitsAsync",onlyAutoStart?"启动自启单元完成":"启动全部单元完成");
        }

        /// <summary>
        /// 停止全部单元
        /// </summary>
        public async Task StopAllUnitsAsync(){
            if(!this.Useable){return;}
            if(this.UnitDictionary.Count<1){return;}
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.StopAllUnitsAsync","正在停止全部单元");
            foreach(KeyValuePair<String,Unit> item in this.UnitDictionary){
                await this.StopUnitAsync(item.Key);
            }
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.StopAllUnitsAsync","停止全部单元完成");
        }

        /// <summary>
        /// 移除全部单元
        /// </summary>
        public async Task RemoveAllUnitsAsync() {
            if(!this.Useable){return;}
            if(this.UnitDictionary.Count<1){return;}
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.RemoveAllUnitsAsync","正在移除全部单元");
            foreach(KeyValuePair<String,Unit> item in this.UnitDictionary){
                await this.RemoveUnitAsync(item.Key);
            }
            LoggerModuleHelper.TryLog("Modules.UnitManageModule.RemoveAllUnitsAsync","移除全部单元完成");
        }
    }
}
