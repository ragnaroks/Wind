using Daemon.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Daemon.Modules {
    /// <summary>单元控制模块</summary>
    public class UnitControlModule:IDisposable{
        private Dictionary<String,Entities.UnitSettings> UnitSettingsDictionary{get;set;}=new Dictionary<String, Entities.UnitSettings>();
        private Dictionary<String,Entities.UnitProcess> UnitProcessDictionary{get;set;}=new Dictionary<String, Entities.UnitProcess>();

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: 释放托管状态(托管对象)。
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design","CA1031:不捕获常规异常类型",Justification = "<挂起>")]
        private Entities.UnitSettings ParseUnitFile(FileInfo unitSettingsFileInfo) {
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
            Entities.UnitSettings unitSettings=JsonSerializer.Deserialize<Entities.UnitSettings>(json);
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
            unitSettings.SetName(unitSettingsFileInfo.GetOriginName());
            Program.LoggerModule.Log("Modules.UnitControlModule.ParseUnitFile",$"单元\"{unitSettingsFileInfo.FullName}\"解析完成");
            return unitSettings;
        }


        /// <summary>
        /// 读取所有单元文件
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design","CA1031:不捕获常规异常类型",Justification = "<挂起>")]
        public Boolean LoadAllUnits(){
            Program.LoggerModule.Log("Modules.UnitControlModule.LoadAllUnits","开始读取所有单元的配置文件");
            //清空所有单元
            this.StopAllUnits();
            this.UnitSettingsDictionary.Clear();
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
            FileInfo[] fileInfoArray;
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
        public void StartAllAutoStartUnits(){
            Program.LoggerModule.Log("Modules.UnitControlModule.StartAllAutoStartUnits","正在启动所有自启单元");
            if(this.UnitSettingsDictionary.Count<1){
                Program.LoggerModule.Log("Modules.UnitControlModule.StartAllAutoStartUnits","没有任何单元配置");
                return;
            }
            foreach(KeyValuePair<String,Entities.UnitSettings> item in this.UnitSettingsDictionary){
                Entities.UnitSettings unitSettings=item.Value;
                if(!unitSettings.AutoStart){continue;}
                Task.Run(()=>{
                    if(unitSettings.AutoStartDelay>0){
                        SpinWait.SpinUntil(()=>false,unitSettings.AutoStartDelay*1000);
                    }
                    this.StartUnit(item.Key);
                });
            }
        }

        /// <summary>
        /// 响应单元进程退出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUnitProcessExited(object sender,EventArgs e) {
            if(this.UnitProcessDictionary==null) {return;}
            Process process=sender as Process;
            Program.LoggerModule.Log("Modules.UnitControlModule.OnUnitProcessExited",$"进程[{process.Id}]主动退出,退出代码[{process.ExitCode}]");
            Int32 processId=process.Id;
            String unitName=null;
            Boolean daemonProcess=false;
            Int32 processExitCode=process.ExitCode;
            //process.Dispose();
            foreach(KeyValuePair<String,Entities.UnitProcess> item in this.UnitProcessDictionary) {
                if(item.Value.Process.Id!=processId){continue;}
                unitName=item.Key;
                if(item.Value.Process!=null) {
                    item.Value.Process.Dispose();
                }
                item.Value.ProcessStartInfo=null;
                if(this.UnitSettingsDictionary.ContainsKey(item.Key) && this.UnitSettingsDictionary[item.Key].DaemonProcess) {daemonProcess=true;}
                Program.LoggerModule.Log("Modules.UnitControlModule.OnUnitProcessExited",$"进程单元\"{item.Key}\"正在退出");
                break;
            }
            if(!String.IsNullOrWhiteSpace(unitName) && this.UnitProcessDictionary.ContainsKey(unitName)){
                this.UnitProcessDictionary.Remove(unitName);
                Program.LoggerModule.Log("Modules.UnitControlModule.OnUnitProcessExited",$"进程单元\"{unitName}\"已退出");
                //移出网络IO统计
                if(Program.UnitNetworkPerformanceTracerModule!=null && processId>0) {
                    Program.UnitNetworkPerformanceTracerModule.RemoveCounter(processId);
                }
                //通知远控客户端
                if(Program.AppSettingsModule.AppSettings.ControlEnable && Program.ControlServerModule.ModuleAvailable){
                    Program.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitStoppedAsync(unitName);
                }
            }
            if(daemonProcess && processExitCode!=0) {
                Program.LoggerModule.Log("Modules.UnitControlModule.OnUnitProcessExited",$"进程单元\"{unitName}\"异常退出,已被守护进程重新启动");
                this.StartUnit(unitName);
            }
        }


        /// <summary>
        /// 刷新单元配置
        /// </summary>
        /// <param name="unitName"></param>
        /// <param name="restartIfUpdate">是否刷新后重启单元</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design","CA1031:不捕获常规异常类型",Justification = "<挂起>")]
        public void ReloadUnit(String unitName,Boolean restartIfUpdate) {
            Program.LoggerModule.Log("Modules.UnitControlModule.ReloadUnit",$"开始刷新单元\"{unitName}\"的配置");
            String unitFIlePath=Program.AppEnvironment.UnitsDirectory+Path.DirectorySeparatorChar+unitName+".json";
            //单元配置文件已丢失,将正在运行的单元退出并移出该单元配置
            if(!File.Exists(unitFIlePath)){
                Program.LoggerModule.Log("Modules.UnitControlModule.ReloadUnit",$"单元\"{unitName}\"配置文件不存在,进行退出操作");
                if(this.UnitSettingsDictionary.ContainsKey(unitName)){
                    this.UnitSettingsDictionary.Remove(unitName);
                }
                this.StopUnit(unitName);
                return;
            }
            //单元配置文件可供更新
            Program.LoggerModule.Log("Modules.UnitControlModule.ReloadUnit",$"单元\"{unitName}\"配置文件存在,重新读取");
            FileInfo fileInfo=null;
            try {
                fileInfo=new FileInfo(unitFIlePath);
            }catch(Exception exception) {
                Program.LoggerModule.Log("Modules.UnitControlModule.ReloadUnit[Error]",$"单元\"{unitName}\"配置文件无法读取,{exception.Message},{exception.StackTrace}");
                return;
            }
            if(fileInfo==null){
                Program.LoggerModule.Log("Modules.UnitControlModule.ReloadUnit[Error]",$"单元\"{unitName}\"配置文件无效");
                return;
            }
            Entities.UnitSettings unitSettings=this.ParseUnitFile(fileInfo);
            if(unitSettings==null){
                Program.LoggerModule.Log("Modules.UnitControlModule.ReloadUnit[Error]",$"单元\"{unitName}\"配置文件解析失败");
                return;
            }
            if(!this.UnitSettingsDictionary.ContainsKey(unitName)) {
                Program.LoggerModule.Log("Modules.UnitControlModule.ReloadUnit[Error]",$"单元\"{unitName}\"配置已失效,进行退出操作");
                this.StopUnit(unitName);
                return;
            }
            this.UnitSettingsDictionary[unitName]=unitSettings;
            Program.LoggerModule.Log("Modules.UnitControlModule.ReloadUnit",$"单元\"{unitName}\"配置已更新");
            //通知客户端单元配置刷新
            if(Program.AppSettingsModule.AppSettings.ControlEnable && Program.ControlServerModule.ModuleAvailable){
                Program.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitSettingsReloadAsync(unitName,unitSettings);
            }
            if(restartIfUpdate){
                this.StopUnit(unitName);
                Task.Run(()=>{
                    SpinWait.SpinUntil(()=>false,1000);
                    this.StartUnit(unitName);
                    Program.LoggerModule.Log("Modules.UnitControlModule.ReloadUnit",$"单元\"{unitName}\"已重启");
                });
            }
        }


        /// <summary>
        /// 启动单元
        /// </summary>
        /// <param name="unitName"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design","CA1031:不捕获常规异常类型",Justification = "<挂起>")]
        public void StartUnit(String unitName){
            if(this.UnitSettingsDictionary.Count<1) {
                Program.LoggerModule.Log("Modules.UnitControlModule.StartUnit[Warning]","当前没有任何单元配置");
                //通知远控客户端
                if(Program.AppSettingsModule.AppSettings.ControlEnable && Program.ControlServerModule.ModuleAvailable){
                    Program.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitStartFailedAsync(unitName);
                }
                return;
            }
            if(!this.UnitSettingsDictionary.ContainsKey(unitName)) {
                Program.LoggerModule.Log("Modules.UnitControlModule.StartUnit[Warning]",$"单元\"{unitName}\"配置不存在");
                //通知远控客户端
                if(Program.AppSettingsModule.AppSettings.ControlEnable && Program.ControlServerModule.ModuleAvailable){
                    Program.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitStartFailedAsync(unitName);
                }
                return;
            }
            if(this.UnitProcessDictionary.ContainsKey(unitName)) {
                Program.LoggerModule.Log("Modules.UnitControlModule.StartUnit[Warning]",$"单元\"{unitName}\"已在运行中");
                //通知远控客户端
                if(Program.AppSettingsModule.AppSettings.ControlEnable && Program.ControlServerModule.ModuleAvailable){
                    Program.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitStartFailedAsync(unitName);
                }
                return;
            }
            Entities.UnitSettings unitSettings=this.UnitSettingsDictionary[unitName];
            Entities.UnitProcess unitProcess=new Entities.UnitProcess();
            unitProcess.SetName(unitSettings.Name);
            unitProcess.ProcessStartInfo=new ProcessStartInfo{UseShellExecute=false,FileName=unitSettings.ExecuteAbsolutePath,WorkingDirectory=unitSettings.WorkAbsoluteDirectory,CreateNoWindow=true,WindowStyle=ProcessWindowStyle.Hidden};
            if(!String.IsNullOrWhiteSpace(unitSettings.ExecuteParams)){unitProcess.ProcessStartInfo.Arguments=unitSettings.ExecuteParams;}
            unitProcess.Process=new Process{StartInfo=unitProcess.ProcessStartInfo,EnableRaisingEvents=true};
            unitProcess.Process.Exited+=OnUnitProcessExited;
            Program.LoggerModule.Log("Modules.UnitControlModule.StartUnit",$"单元\"{unitSettings.Name}\"所需数据已构造完成,进入启动队列");
            unitProcess.State=2;
            Boolean started=false;
            try {
                started=unitProcess.Process.Start();
            }catch(Exception exception){
                Program.LoggerModule.Log("Modules.UnitControlModule.StartUnit[Error]",$"单元\"{unitSettings.Name}\"启动失败,{exception.Message},{exception.StackTrace}");
                unitProcess.State=1;
                unitProcess.ProcessStartInfo=null;
                unitProcess.Process.Dispose();
                //通知远控客户端
                if(Program.AppSettingsModule.AppSettings.ControlEnable && Program.ControlServerModule.ModuleAvailable){
                    Program.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitStartFailedAsync(unitName);
                }
                return;
            }
            if(!started) {
                Program.LoggerModule.Log("Modules.UnitControlModule.StartUnit[Error]",$"单元\"{unitSettings.Name}\"启动失败");
                unitProcess.State=1;
                unitProcess.ProcessStartInfo=null;
                unitProcess.Process.Dispose();
                //通知远控客户端
                if(Program.AppSettingsModule.AppSettings.ControlEnable && Program.ControlServerModule.ModuleAvailable){
                    Program.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitStartFailedAsync(unitName);
                }
                return;
            }
            Program.LoggerModule.Log("Modules.UnitControlModule.StartUnit",$"单元\"{unitSettings.Name}\"已启动");
            unitProcess.State=3;
            unitProcess.ProcessId=unitProcess.Process.Id;
            this.UnitProcessDictionary.Add(unitProcess.Name,unitProcess);
            //加入网络IO统计
            if(Program.UnitNetworkPerformanceTracerModule!=null && unitSettings.FetchNetworkUsage) {
                Program.UnitNetworkPerformanceTracerModule.AddCounter(unitProcess.ProcessId);
            }
            //通知客户端单元启动
            if(Program.AppSettingsModule.AppSettings.ControlEnable && Program.ControlServerModule.ModuleAvailable){
                Program.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitStartedAsync(unitName,unitProcess);
            }
        }


        /// <summary>
        /// 停止单元
        /// </summary>
        /// <param name="unitName"></param>
        /// <param name="stopBySerivceExited">是否服务主机退出导致的停止单元</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design","CA1031:不捕获常规异常类型",Justification = "<挂起>")]
        public void StopUnit(String unitName,Boolean stopBySerivceExited=false) {
            if(this.UnitProcessDictionary.Count<1){
                if(!stopBySerivceExited) {
                    Program.LoggerModule.Log("Modules.UnitControlModule.StopUnit[Warning]","当前没有任何运行中的单元");
                    //停止客户端停止单元失败
                    if(Program.AppSettingsModule.AppSettings.ControlEnable && Program.ControlServerModule.ModuleAvailable){
                        Program.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitStopFailedAsync(unitName);
                    }
                }
                return;
            }
            if(!this.UnitProcessDictionary.ContainsKey(unitName)){
                if(!stopBySerivceExited) {
                    Program.LoggerModule.Log("Modules.UnitControlModule.StopUnit[Warning]",$"单元\"{unitName}\"未运行");
                    if(Program.AppSettingsModule.AppSettings.ControlEnable && Program.ControlServerModule.ModuleAvailable){
                        Program.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitStopFailedAsync(unitName);
                    }
                }
                return;
            }
            if(!stopBySerivceExited) {
                Program.LoggerModule.Log("Modules.UnitControlModule.StopUnit",$"单元\"{unitName}\"正在执行停止流程");
            }
            try {
                if(this.UnitSettingsDictionary[unitName].HaveChildProcesses) {
                    this.UnitProcessDictionary[unitName].Process.KillTree();
                } else {
                    this.UnitProcessDictionary[unitName].Process.Kill();
                }
            }catch(Exception exception){
                //异常之后也要继续停止流程
                if(!stopBySerivceExited) {
                    Program.LoggerModule.Log("Modules.UnitControlModule.StopUnit[Error]",$"单元\"{unitName}\"正在执行停止流程时异常,{exception.Message},{exception.StackTrace}");
                }
            }
            Int32 processId=0;
            if(this.UnitProcessDictionary.ContainsKey(unitName)) {
                processId=this.UnitProcessDictionary[unitName].ProcessId;
                this.UnitProcessDictionary[unitName].Process.Dispose();
                this.UnitProcessDictionary[unitName].State=1;
                this.UnitProcessDictionary[unitName].ProcessStartInfo=null;
                this.UnitProcessDictionary.Remove(unitName);
            }
            if(!stopBySerivceExited) {
                Program.LoggerModule.Log("Modules.UnitControlModule.StopUnit",$"单元\"{unitName}\"执行停止流程完毕");
                //移出网络IO统计
                if(Program.UnitNetworkPerformanceTracerModule!=null && processId>0) {
                    Program.UnitNetworkPerformanceTracerModule.RemoveCounter(processId);
                }
                //通知客户端单元停止
                if(Program.AppSettingsModule.AppSettings.ControlEnable && Program.ControlServerModule.ModuleAvailable){
                    Program.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitStoppedAsync(unitName);
                }
            }
        }

        /// <summary>
        /// 启动所有单元
        /// </summary>
        public void StartAllUnits(){
            Program.LoggerModule.Log("Modules.UnitControlModule.StartAllUnits","正在启动所有单元");
            if(this.UnitSettingsDictionary.Count<1){
                Program.LoggerModule.Log("Modules.UnitControlModule.StartAllUnits","没有任何单元配置");
                return;
            }
            foreach(KeyValuePair<String,Entities.UnitSettings> item in this.UnitSettingsDictionary){
                Task.Run(()=>{
                    this.StartUnit(item.Key);
                });
            }
        }

        /// <summary>
        /// 刷新所有单元配置文件
        /// </summary>
        /// <param name="restartIfUpdate">是否刷新后重启单元</param>
        public void ReloadAllUnits(Boolean restartIfUpdate){
            if(this.UnitSettingsDictionary.Count<1){return;}
            Program.LoggerModule.Log("Modules.UnitControlModule.ReloadAllUnits","开始刷新所有单元配置");
            foreach(KeyValuePair<String,Entities.UnitSettings> item in this.UnitSettingsDictionary) {
                this.ReloadUnit(item.Key,restartIfUpdate);
            }
        }

        /// <summary>
        /// 停止所有单元
        /// </summary>
        /// <param name="stopBySerivceExited">是否服务主机退出导致的停止所有单元</param>
        public void StopAllUnits(Boolean stopBySerivceExited=false){
            if(this.UnitProcessDictionary.Count<1){
                if(!stopBySerivceExited){
                    Program.LoggerModule.Log("Modules.UnitControlModule.StopAllUnits[Warning]","当前没有运行中的单元");
                }
                return;
            }
            if(!stopBySerivceExited){
                Program.LoggerModule.Log("Modules.UnitControlModule.StopAllUnits","开始停止所有运行中的单元");
            }
            foreach(KeyValuePair<String,Entities.UnitProcess> item in this.UnitProcessDictionary) {
                this.StopUnit(item.Key,stopBySerivceExited);
            }
        }

        /// <summary>
        /// 获取所有单元的状态
        /// </summary>
        /// <returns></returns>
        public List<Entities.UnitStatus> FetchAllUnitsStatus() {
            if(this.UnitSettingsDictionary.Count<1) {
                Program.LoggerModule.Log("Modules.UnitControlModule.FetchAllUnitsStatus[Warning]","当前没有任何有效单元");
                return null;
            }
            Boolean canForEachUnitProcessDictionary=this.UnitProcessDictionary!=null && this.UnitProcessDictionary.Count>0;
            List<Entities.UnitStatus> unitStatusList=new List<Entities.UnitStatus>();
            foreach(KeyValuePair<String,Entities.UnitSettings> item in this.UnitSettingsDictionary) {
                Entities.UnitStatus unitStatus=new Entities.UnitStatus{UnitName=item.Key,UnitSettings=item.Value};
                if(canForEachUnitProcessDictionary && this.UnitProcessDictionary.ContainsKey(item.Key)){
                    unitStatus.UnitProcess=this.UnitProcessDictionary[item.Key];
                }
                unitStatusList.Add(unitStatus);
            }
            return unitStatusList;
        }

        /// <summary>
        /// 获取单元状态
        /// </summary>
        /// <param name="unitName"></param>
        /// <returns></returns>
        public Entities.UnitStatus FetchUnitStatus(String unitName) {
            if(this.UnitSettingsDictionary.Count<1) {
                Program.LoggerModule.Log("Modules.UnitControlModule.FetchUnitStatus[Warning]","当前没有任何有效单元");
                return null;
            }
            if(!this.UnitSettingsDictionary.ContainsKey(unitName)){return null;}
            Entities.UnitStatus unitStatus=new Entities.UnitStatus{UnitName=unitName,UnitSettings=this.UnitSettingsDictionary[unitName]};
            if(this.UnitProcessDictionary.Count>0 && this.UnitProcessDictionary.ContainsKey(unitName)) {
                unitStatus.UnitProcess=this.UnitProcessDictionary[unitName];
            }
            return unitStatus;
        }

        /// <summary>
        /// 获取单元进程数量
        /// </summary>
        /// <returns></returns>
        public Int32 GetUnitProcessCount()=>this.UnitProcessDictionary.Count;
        /// <summary>
        /// 获取单元配置数量
        /// </summary>
        /// <returns></returns>
        public Int32 GetUnitSettingsCount()=>this.UnitSettingsDictionary.Count;
    }
}