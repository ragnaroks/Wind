using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Daemon.Modules {
    /// <summary>单元控制模块</summary>
    public class UnitControlModule:IDisposable{
        private Boolean CanLoadAllUnits=true;
        private Boolean CanStartAllAutoStartUnits=true;
        private Boolean CanStopAllUnits=false;
        private Dictionary<String,Entities.UnitSettings> UnitSettingsDictionary=new Dictionary<String, Entities.UnitSettings>();
        private Dictionary<String,Entities.UnitProcess> UnitProcessDictionary=new Dictionary<String, Entities.UnitProcess>();

        public UnitControlModule() {
            if(!this.LoadAllUnits()) {
                ConsoleColor cc=Console.ForegroundColor;
                Console.ForegroundColor=ConsoleColor.Yellow;
                Console.WriteLine("Modules.UnitControlModule.UnitControlModule => 没有正常读取到单元列表");
                Console.ForegroundColor=cc;
                Program.LoggerModule.Log("Modules.UnitControlModule.UnitControlModule[Warning]","没有正常读取到单元列表");
            } else {
                this.StartAllAutoStartUnits();
            }
            this.CanStopAllUnits=true;
            Task.Run(async()=>{
                await Task.Delay(30*1000);
                Console.WriteLine(this.UnitProcessDictionary.Count);
                this.StopAllUnits();
                Console.WriteLine(this.UnitProcessDictionary.Count);
            });
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: 释放托管状态(托管对象)。
                    this.StopAllUnits();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                this.UnitSettingsDictionary=null;
                this.UnitProcessDictionary=null;

                disposedValue=true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~UnitControlModule(){
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

        /// <summary>
        /// 解析单元配置文件
        /// </summary>
        /// <param name="unitSettingsFileInfo"></param>
        /// <returns></returns>
        private Entities.UnitSettings ParseUnitFile(FileInfo unitSettingsFileInfo) {
            //Span<Byte> bytes=new Span<Byte>();
            Byte[] bytes;
            FileStream fs=null;
            try {
                fs=unitSettingsFileInfo.Open(FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
                bytes=new Byte[fs.Length];
                fs.Read(bytes);
                fs.Close();
            }catch(Exception exception) {
                Program.LoggerModule.Log("Modules.UnitControlModule.ParseUnitFile[Error]",$"单元\"{unitSettingsFileInfo.FullName}\"解析失败,{exception.Message},{exception.StackTrace}");
                return null;
            } finally {
                if(fs!=null){fs.Dispose();}
            }
            String json=Encoding.UTF8.GetString(bytes);
            if(String.IsNullOrWhiteSpace(json)) {
                Program.LoggerModule.Log("Modules.UnitControlModule.ParseUnitFile[Error]",$"单元\"{unitSettingsFileInfo.FullName}\"解析失败,文件内容为空");
                return null;
            }
            Entities.UnitSettings unitSettings=Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.UnitSettings>(json);
            if(unitSettings==null) {
                Program.LoggerModule.Log("Modules.UnitControlModule.ParseUnitFile[Error]",$"单元\"{unitSettingsFileInfo.FullName}\"解析失败,文件反序列化失败");
                return null;
            }
            if(String.IsNullOrWhiteSpace(unitSettings.Description)) {
                Program.LoggerModule.Log("Modules.UnitControlModule.ParseUnitFile[Warning]",$"单元\"{unitSettingsFileInfo.FullName}\"解析警告,不建议 Description 配置项为空");
            }
            if(String.IsNullOrWhiteSpace(unitSettings.ExecuteAbsolutePath)) {
                Program.LoggerModule.Log("Modules.UnitControlModule.ParseUnitFile[Error]",$"单元\"{unitSettingsFileInfo.FullName}\"解析失败,ExecuteAbsolutePath 配置项为空");
                return null;
            }
            if(!File.Exists(unitSettings.ExecuteAbsolutePath)) {
                Program.LoggerModule.Log("Modules.UnitControlModule.ParseUnitFile[Error]",$"单元\"{unitSettingsFileInfo.FullName}\"解析失败,ExecuteAbsolutePath 配置项无效,可执行文件不存在或没有读取权限");
                return null;
            }
            if(String.IsNullOrWhiteSpace(unitSettings.WorkAbsoluteDirectory)) {
                Program.LoggerModule.Log("Modules.UnitControlModule.ParseUnitFile[Error]",$"单元\"{unitSettingsFileInfo.FullName}\"解析失败,WorkAbsoluteDirectory 配置项为空");
                return null;
            }
            if(!Directory.Exists(unitSettings.WorkAbsoluteDirectory)) {
                Program.LoggerModule.Log("Modules.UnitControlModule.ParseUnitFile[Error]",$"单元\"{unitSettingsFileInfo.FullName}\"解析失败,WorkAbsoluteDirectory 配置项无效,可执行文件工作目录不存在或没有读取权限");
                return null;
            }
            unitSettings.SetName(unitSettingsFileInfo.Name);
            Program.LoggerModule.Log("Modules.UnitControlModule.ParseUnitFile",$"单元\"{unitSettingsFileInfo.FullName}\"解析完成");
            return unitSettings;
        }

        /// <summary>
        /// 读取所有单元文件
        /// </summary>
        /// <returns></returns>
        private Boolean LoadAllUnits(){
            if(!this.CanLoadAllUnits){return false;}
            this.CanLoadAllUnits=false;
            Program.LoggerModule.Log("Modules.UnitControlModule.LoadAllUnits","开始读取所有单元的配置文件");
            //目录不存在或首次启动
            DirectoryInfo directoryInfo=null;
            if(!Directory.Exists(Program.AppEnvironment.UnitsDirectory)){
                Program.LoggerModule.Log("Modules.UnitControlModule.LoadAllUnits[Warning]",$"单元配置文件存放目录\"{Program.AppEnvironment.UnitsDirectory}\"不存在,正在尝试创建");
                try {
                    directoryInfo=Directory.CreateDirectory(Program.AppEnvironment.UnitsDirectory);
                }catch(Exception exception) {
                    Program.LoggerModule.Log("Modules.UnitControlModule.LoadAllUnits[Error]",$"单元配置文件存放目录\"{Program.AppEnvironment.UnitsDirectory}\"不存在,且创建失败,{exception.Message},{exception.StackTrace}");
                    return false;
                }
                Program.LoggerModule.Log("Modules.UnitControlModule.LoadAllUnits[Warning]",$"单元配置文件存放目录\"{Program.AppEnvironment.UnitsDirectory}\"不存在,已创建成功");
                return false;
            }
            if(directoryInfo==null) {
                try {
                    directoryInfo=new DirectoryInfo(Program.AppEnvironment.UnitsDirectory);
                }catch(Exception exception) {
                    Program.LoggerModule.Log("Modules.UnitControlModule.LoadAllUnits[Error]",$"单元配置文件存放目录\"{Program.AppEnvironment.UnitsDirectory}\"拒绝读取,{exception.Message},{exception.StackTrace}");
                    return false;
                }
            }
            //查找单元
            FileInfo[] fileInfoArray=null;
            try{
                fileInfoArray=directoryInfo.GetFiles("*.json",SearchOption.TopDirectoryOnly);
            }catch(Exception exception) {
                Program.LoggerModule.Log("Modules.UnitControlModule.LoadAllUnits[Error]",$"单元配置文件存放目录\"{Program.AppEnvironment.UnitsDirectory}\"拒绝读取,{exception.Message},{exception.StackTrace}");
                return false;
            }
            if(fileInfoArray==null || fileInfoArray.Length<1){
                Program.LoggerModule.Log("Modules.UnitControlModule.LoadAllUnits[Warning]",$"未读取到任何有效单元");
                return false;
            }
            //遍历单元
            foreach(FileInfo fileInfo in fileInfoArray) {
                Entities.UnitSettings unitSettings=this.ParseUnitFile(fileInfo);
                if(unitSettings==null || String.IsNullOrWhiteSpace(unitSettings.Name)) {
                    Program.LoggerModule.Log("Modules.UnitControlModule.LoadAllUnits[Warning]",$"单元\"{fileInfo.FullName}\"读取失败,已跳过");
                    continue;
                }
                this.UnitSettingsDictionary.Add(unitSettings.Name,unitSettings);
                Program.LoggerModule.Log("Modules.UnitControlModule.LoadAllUnits",$"单元\"{fileInfo.FullName}\"读取成功,已加入单元列表");
            }
            return true;
        }

        /// <summary>
        /// 启动所有自启单元
        /// </summary>
        /// <param name="unitSettingsDictionary"></param>
        private void StartAllAutoStartUnits(){
            if(!this.CanStartAllAutoStartUnits){return;}
            this.CanStartAllAutoStartUnits=false;
            Program.LoggerModule.Log("Modules.UnitControlModule.StartAllAutoStartUnits","正在启动所有自启单元");
            if(this.UnitSettingsDictionary==null || this.UnitSettingsDictionary.Count<1){
                Program.LoggerModule.Log("Modules.UnitControlModule.StartAllAutoStartUnits","没有任何自启单元");
                return;
            }
            foreach(KeyValuePair<String,Entities.UnitSettings> item in this.UnitSettingsDictionary) {
                Entities.UnitSettings unitSettings=item.Value;
                if(!unitSettings.AutoStart){continue;}
                Entities.UnitProcess unitProcess=new Entities.UnitProcess();
                unitProcess.SetName(unitSettings.Name);
                unitProcess.ProcessStartInfo=new ProcessStartInfo{UseShellExecute=false,FileName=unitSettings.ExecuteAbsolutePath,WorkingDirectory=unitSettings.WorkAbsoluteDirectory,CreateNoWindow=true,WindowStyle=ProcessWindowStyle.Hidden};
                if(!String.IsNullOrWhiteSpace(unitSettings.ExecuteParams)){unitProcess.ProcessStartInfo.Arguments=unitSettings.ExecuteParams;}
                unitProcess.Process=new Process{StartInfo=unitProcess.ProcessStartInfo,EnableRaisingEvents=true};
                unitProcess.Process.Exited+=OnUnitProcessExited;
                Task.Run(()=>{
                    Program.LoggerModule.Log("Modules.UnitControlModule.StartAllAutoStartUnits",$"\"{unitSettings.Name}\"单元所需数据已构造完成,进入启动队列");
                    unitProcess.State=Enums.UnitProcess.State.正在启动;
                    if(unitSettings.AutoStartDelay>0){Thread.Sleep(unitSettings.AutoStartDelay*1000);}
                    try {
                        unitProcess.Process.Start();
                    }catch(Exception exception){
                        Program.LoggerModule.Log("Modules.UnitControlModule.StartAllAutoStartUnits[Error]",$"\"{unitSettings.Name}\"单元启动失败,{exception.Message},{exception.StackTrace}");
                        unitProcess.State=Enums.UnitProcess.State.停止;
                        unitProcess.ProcessStartInfo=null;
                        unitProcess.Process.Dispose();
                    }
                    Program.LoggerModule.Log("Modules.UnitControlModule.StartAllAutoStartUnits",$"\"{unitSettings.Name}\"单元已启动");
                    unitProcess.State=Enums.UnitProcess.State.运行中;
                    this.UnitProcessDictionary.Add(unitProcess.Name,unitProcess);
                });
            }
        }

        /// <summary>
        /// 单元进程主动退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUnitProcessExited(object sender,EventArgs e) {
            Process process=sender as Process;
            String unitName=null;
            foreach(KeyValuePair<String,Entities.UnitProcess> item in this.UnitProcessDictionary) {
                if(item.Value.Process==null){continue;}
                if(item.Value.Process.Id!=process.Id){continue;}
                item.Value.Process.Dispose();
                item.Value.Process=null;
                item.Value.ProcessStartInfo=null;
                Program.LoggerModule.Log("Modules.UnitControlModule.OnUnitProcessExited",$"单元\"{item.Key}\"正在退出");
                unitName=item.Key;
                break;
            }
            if(!String.IsNullOrWhiteSpace(unitName)){
                this.UnitProcessDictionary.Remove(unitName);
                Program.LoggerModule.Log("Modules.UnitControlModule.OnUnitProcessExited",$"单元\"{unitName}\"已退出");
            }
        }

        /// <summary>
        /// 停止所有单元
        /// </summary>
        public void StopAllUnits(){
            if(!this.CanStopAllUnits){return;}
            //List<String> stoppedUnitNames=new List<String>();
            foreach(KeyValuePair<String,Entities.UnitProcess> item in this.UnitProcessDictionary) {
                if(item.Value.Process==null){continue;}
                Program.LoggerModule.Log("Modules.UnitControlModule.StopAllUnits",$"单元\"{item.Key}\"正在强制停止");
                item.Value.Process.Kill(true);//kill后自动释放
                //item.Value.Process.Dispose();
                //item.Value.Process=null;
                item.Value.ProcessStartInfo=null;
                Program.LoggerModule.Log("Modules.UnitControlModule.StopAllUnits",$"单元\"{item.Key}\"已强制停止");
                //stoppedUnitNames.Add(item.Key);
            }
            //this.UnitProcessDictionary.Clear();
        }
    }
}
