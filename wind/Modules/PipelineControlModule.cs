using Google.Protobuf;
using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using wind.Entities.Common;
using wind.Entities.Protobuf;
using wind.Helpers;

namespace wind.Modules {
    /// <summary>本地(命名管道)管理模块</summary>
    public class PipelineControlModule{

        /// <summary>
        /// windctl status unitKey
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static Byte[] OnStatus(Byte[] request){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnStatus","开始处理 status 指令");
            StatusRequestProtobuf statusRequestProtobuf=StatusRequestProtobuf.Parser.ParseFrom(request);
            StatusResponseProtobuf statusResponseProtobuf=new StatusResponseProtobuf{Type=2001,UnitKey=statusRequestProtobuf.UnitKey};
            //无效unit
            if(String.IsNullOrWhiteSpace(statusRequestProtobuf.UnitKey)){
                statusResponseProtobuf.NoExecuteMessage="unitKey invalid";
                return statusResponseProtobuf.ToByteArray();
            }
            if(!Program.UnitManageModule.Useable){
                statusResponseProtobuf.NoExecuteMessage="unit manager not available";
                return statusResponseProtobuf.ToByteArray();
            }
            Entities.Common.Unit unit=Program.UnitManageModule.GetUnit(statusRequestProtobuf.UnitKey);
            if(unit==null) {
                statusResponseProtobuf.NoExecuteMessage="unit not found";
                return statusResponseProtobuf.ToByteArray();
            }
            //
            UnitProcessProtobuf processProtobuf=new UnitProcessProtobuf();
            if(unit.State==2) {
                processProtobuf.Id=unit.ProcessId;
                processProtobuf.StartTime=unit.Process.StartTime.ToLocalTimestamp();//这里的starttime是localtime
            }
            UnitSettingsProtobuf unitSettingsProtobuf=new UnitSettingsProtobuf{
                Name=unit.Settings.Name,Description=unit.Settings.Description,Type=unit.Settings.Type,AbsoluteExecutePath=unit.Settings.AbsoluteExecutePath,
                AbsoluteWorkDirectory=unit.Settings.AbsoluteWorkDirectory,Arguments=String.IsNullOrWhiteSpace(unit.Settings.Arguments)?String.Empty:unit.Settings.Arguments,
                AutoStart=unit.Settings.AutoStart,AutoStartDelay=unit.Settings.AutoStartDelay,RestartWhenException=unit.Settings.RestartWhenException,
                MonitorPerformanceUsage=unit.Settings.MonitorPerformanceUsage,MonitorNetworkUsage=unit.Settings.MonitorNetworkUsage};
            UnitSettingsProtobuf unitRunningSettingsProtobuf=new UnitSettingsProtobuf();
            if(unit.State==2){
                unitRunningSettingsProtobuf=new UnitSettingsProtobuf{
                    Name=unit.RunningSettings.Name,Description=unit.RunningSettings.Description,Type=unit.RunningSettings.Type,
                    AbsoluteExecutePath=unit.RunningSettings.AbsoluteExecutePath,AbsoluteWorkDirectory=unit.RunningSettings.AbsoluteWorkDirectory,
                    Arguments=String.IsNullOrWhiteSpace(unit.RunningSettings.Arguments)?String.Empty:unit.RunningSettings.Arguments,
                    AutoStart=unit.RunningSettings.AutoStart,AutoStartDelay=unit.RunningSettings.AutoStartDelay,
                    RestartWhenException=unit.RunningSettings.RestartWhenException,MonitorPerformanceUsage=unit.RunningSettings.MonitorPerformanceUsage,
                    MonitorNetworkUsage=unit.RunningSettings.MonitorNetworkUsage};
            }            
            UnitPerformanceCounterProtobuf unitPerformanceCounterProtobuf=new UnitPerformanceCounterProtobuf();
            if(Program.UnitPerformanceCounterModule.Useable && unit.State==2 && unitRunningSettingsProtobuf.MonitorPerformanceUsage){
                unitPerformanceCounterProtobuf.CPU=Program.UnitPerformanceCounterModule.GetCpuValue(unit.ProcessId);
                unitPerformanceCounterProtobuf.RAM=Program.UnitPerformanceCounterModule.GetRamValue(unit.ProcessId);
            }
            UnitNetworkCounterProtobuf unitNetworkCounterProtobuf=new UnitNetworkCounterProtobuf();
            if(Program.UnitNetworkCounterModule.Useable && unit.State==2 && unitRunningSettingsProtobuf.MonitorNetworkUsage){
                UnitNetworkCounter unitNetworkCounter=Program.UnitNetworkCounterModule.GetValue(unit.ProcessId);
                if(unitNetworkCounter!=null){
                    unitNetworkCounterProtobuf.SendSpeed=unitNetworkCounter.SendSpeed;
                    unitNetworkCounterProtobuf.ReceiveSpeed=unitNetworkCounter.ReceiveSpeed;
                    unitNetworkCounterProtobuf.TotalSent=unitNetworkCounter.TotalSent;
                    unitNetworkCounterProtobuf.TotalReceived=unitNetworkCounter.TotalReceived;
                }
            }
            UnitProtobuf unitProtobuf=new UnitProtobuf{
                Key=unit.Key,State=unit.State,
                SettingsFilePath=String.Concat(Program.AppEnvironment.UnitsDirectory,Path.DirectorySeparatorChar,unit.Key,".json"),
                ProcessProtobuf=processProtobuf,SettingsProtobuf=unitSettingsProtobuf,RunningSettingsProtobuf=unitRunningSettingsProtobuf,
                PerformanceCounterProtobuf=unitPerformanceCounterProtobuf,NetworkCounterProtobuf=unitNetworkCounterProtobuf};
            statusResponseProtobuf.Executed=true;
            statusResponseProtobuf.UnitProtobuf=unitProtobuf;
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnStatus","已处理 status 指令");
            return statusResponseProtobuf.ToByteArray();
        }
        /// <summary>
        /// windctl start unitKey
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static Byte[] OnStart(Byte[] request){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnStart","开始处理 start 指令");
            StartRequestProtobuf startRequestProtobuf=StartRequestProtobuf.Parser.ParseFrom(request);
            StartResponseProtobuf startResponseProtobuf=new StartResponseProtobuf{Type=2002,UnitKey=startRequestProtobuf.UnitKey};
            //无效unit
            if(String.IsNullOrWhiteSpace(startRequestProtobuf.UnitKey)){
                startResponseProtobuf.NoExecuteMessage="unitKey invalid";
                return startResponseProtobuf.ToByteArray();
            }
            if(!Program.UnitManageModule.Useable){
                startResponseProtobuf.NoExecuteMessage="unit manager not available";
                return startResponseProtobuf.ToByteArray();
            }
            Entities.Common.Unit unit=Program.UnitManageModule.GetUnit(startRequestProtobuf.UnitKey);
            if(unit==null) {
                startResponseProtobuf.NoExecuteMessage="unit not found";
                return startResponseProtobuf.ToByteArray();
            }
            if(unit.State==1 || unit.State==2) {
                startResponseProtobuf.NoExecuteMessage="unit has been started";
                return startResponseProtobuf.ToByteArray();
            }
            if(Program.UnitManageModule.StartUnit(startRequestProtobuf.UnitKey,false)) {
                startResponseProtobuf.Executed=true;
            } else {
                startResponseProtobuf.NoExecuteMessage="start unit failed";
            }
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnStart","已处理 start 指令");
            return startResponseProtobuf.ToByteArray();
        }
        /// <summary>
        /// windctl stop unitKey
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static Byte[] OnStop(Byte[] request){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnStop","开始处理 stop 指令");
            StopRequestProtobuf stopRequestProtobuf=StopRequestProtobuf.Parser.ParseFrom(request);
            StopResponseProtobuf stopResponseProtobuf=new StopResponseProtobuf{Type=2003,UnitKey=stopRequestProtobuf.UnitKey};
            //无效unit
            if(String.IsNullOrWhiteSpace(stopRequestProtobuf.UnitKey)){
                stopResponseProtobuf.NoExecuteMessage="unitKey invalid";
                return stopResponseProtobuf.ToByteArray();
            }
            if(!Program.UnitManageModule.Useable){
                stopResponseProtobuf.NoExecuteMessage="unit manager not available";
                return stopResponseProtobuf.ToByteArray();
            }
            Entities.Common.Unit unit=Program.UnitManageModule.GetUnit(stopRequestProtobuf.UnitKey);
            if(unit==null) {
                stopResponseProtobuf.NoExecuteMessage="unit not found";
                return stopResponseProtobuf.ToByteArray();
            }
            if(unit.State==3 || unit.State==0) {
                stopResponseProtobuf.NoExecuteMessage="unit has been stopped";
                return stopResponseProtobuf.ToByteArray();
            }
            if(Program.UnitManageModule.StopUnit(stopRequestProtobuf.UnitKey)) {
                stopResponseProtobuf.Executed=true;
            } else {
                stopResponseProtobuf.NoExecuteMessage="stop unit failed";
            }
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnStop","已处理 stop 指令");
            return stopResponseProtobuf.ToByteArray();
        }
        /// <summary>
        /// windctl restart unitKey
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static Byte[] OnRestart(Byte[] request){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnRestart","开始处理 restart 指令");
            RestartRequestProtobuf restartRequestProtobuf=RestartRequestProtobuf.Parser.ParseFrom(request);
            RestartResponseProtobuf restartResponseProtobuf=new RestartResponseProtobuf{Type=2004,UnitKey=restartRequestProtobuf.UnitKey};
            //无效unit
            if(String.IsNullOrWhiteSpace(restartRequestProtobuf.UnitKey)){
                restartResponseProtobuf.NoExecuteMessage="unitKey invalid";
                return restartResponseProtobuf.ToByteArray();
            }
            if(!Program.UnitManageModule.Useable){
                restartResponseProtobuf.NoExecuteMessage="unit manager not available";
                return restartResponseProtobuf.ToByteArray();
            }
            Entities.Common.Unit unit=Program.UnitManageModule.GetUnit(restartResponseProtobuf.UnitKey);
            if(unit==null) {
                restartResponseProtobuf.NoExecuteMessage="unit not found";
                return restartResponseProtobuf.ToByteArray();
            }
            if(Program.UnitManageModule.RestartUnit(restartRequestProtobuf.UnitKey)) {
                restartResponseProtobuf.Executed=true;
            } else {
                restartResponseProtobuf.NoExecuteMessage="restart unit failed";
            }
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnRestart","已处理 restart 指令");
            return restartResponseProtobuf.ToByteArray();
        }

        /// <summary>
        /// windctl daemon-version
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        private static Byte[] OnDaemonVersion(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnDaemonVersion","开始处理 daemon-version 指令");
            /*
            Version version=Assembly.GetExecutingAssembly().GetName().Version;
            DaemonVersionResponseProtobuf daemonVersionResponseProtobuf=new DaemonVersionResponseProtobuf{
                Type=2200,MajorVersion=version.Major,MinorVersion=version.Minor,BuildVersion=version.Build,RevisionVersion=version.Revision};
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnDaemonVersion","已处理 daemon-version 指令");
            return daemonVersionResponseProtobuf.ToByteArray();
            */
            return null;
        }
        /// <summary>
        /// windctl daemon-status
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        private static Byte[] OnDaemonStatus(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnDaemonStatus","开始处理 daemon-status 指令");
            /*
            ProcessProtobuf processProtobuf=new ProcessProtobuf{Id=Program.AppProcess.Id,StartTime=Program.AppProcess.StartTime.ToLocalTimestamp()};
            UnitPerformanceCounterProtobuf unitPerformanceCounter=new UnitPerformanceCounterProtobuf{RAM=0F,CPU=0F};
            UnitNetworkCounterProtobuf unitNetworkCounter =new UnitNetworkCounterProtobuf { SendSpeed=0,ReceiveSpeed=0,TotalSent=0,TotalReceived=0};
            if(Program.UnitPerformanceCounterModule.Useable) {
                unitPerformanceCounter.CPU=Program.UnitPerformanceCounterModule.GetCpuValue(Program.AppProcess.Id);
                unitPerformanceCounter.RAM=Program.UnitPerformanceCounterModule.GetRamValue(Program.AppProcess.Id);
            }
            if(Program.UnitNetworkCounterModule.Useable) {
                UnitNetworkCounter counter =Program.UnitNetworkCounterModule.GetValue(Program.AppProcess.Id);
                unitNetworkCounter.SendSpeed=counter.SendSpeed;
                unitNetworkCounter.ReceiveSpeed=counter.ReceiveSpeed;
                unitNetworkCounter.TotalSent=counter.TotalSent;
                unitNetworkCounter.TotalReceived=counter.TotalReceived;
            }
            DaemonProtobuf daemonProtobuf=new DaemonProtobuf{
                Name="wind",Description="a systemd for windows",
                ProcessProtobuf=processProtobuf,PerformanceCounterProtobuf=unitPerformanceCounter,NetworkCounterProtobuf=unitNetworkCounter,
                AbsoluteExecutePath=Program.AppEnvironment.BaseDirectory+"wind.exe",AbsoluteWorkDirectory=Program.AppEnvironment.BaseDirectory};
            DaemonStatusResponseProtobuf daemonStatusResponseProtobuf=new DaemonStatusResponseProtobuf{Type=2201,DaemonProtobuf=daemonProtobuf};
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnDaemonStatus","已处理 daemon-status 指令");
            return daemonStatusResponseProtobuf.ToByteArray();
            */
            return null;
        }
        /// <summary>
        /// windctl daemon-shutdown
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        private static Byte[] OnDaemonShutdown(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnDaemonShutdown","开始处理 daemon-shutdown 指令");
            /*
            DaemonShutdownResponseProtobuf daemonShutdownResponseProtobuf=new DaemonShutdownResponseProtobuf{Type=2299};
            Task.Run(()=>{
                SpinWait.SpinUntil(()=>false,1000);
                Program.DaemonServiceController.Stop();
                SpinWait.SpinUntil(()=>false,3000);
                Environment.Exit(0);
            });
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnDaemonShutdown","已处理 daemon-shutdown 指令");
            return daemonShutdownResponseProtobuf.ToByteArray();
            */
            return null;
        }


        /// <summary>
        /// 处理消息 0x05
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        [Obsolete]
        private static Byte[] OnMessageLoad(Byte[] bytes){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageLoad","开始处理 load 指令");
            //解析 unitKey
            String unitKey;
            try {
                unitKey=Encoding.UTF8.GetString(bytes,8,104-8).TrimEnd().TrimEnd(new Char[]{'\0'});
            }catch(Exception exception){
                Helpers.LoggerModuleHelper.TryLog(
                    "Modules.PipelineControlModule.OnMessageLoad[Error]",
                    $"解析 unitKey 异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            if(String.IsNullOrWhiteSpace(unitKey)){
                Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageLoad[Error]","解析 unitKey 失败");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Boolean b1=Program.UnitManageModule.LoadUnit(unitKey);
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageLoad","已处理 load 指令");
            return new Byte[8]{0x05,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes($"unit load {(b1?"success":"fail")}")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x06
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        [Obsolete]
        private static Byte[] OnMessageRemove(Byte[] bytes){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRemove","开始处理 remove 指令");
            //解析 unitKey
            String unitKey;
            try {
                unitKey=Encoding.UTF8.GetString(bytes,8,104-8).TrimEnd().TrimEnd(new Char[]{'\0'});
            }catch(Exception exception){
                Helpers.LoggerModuleHelper.TryLog(
                    "Modules.PipelineControlModule.OnMessageRemove[Error]",
                    $"解析 unitKey 异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            if(String.IsNullOrWhiteSpace(unitKey)){
                Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRemove[Error]","解析 unitKey 失败");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            if(Program.UnitManageModule.GetUnit(unitKey)==null) {
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unit is not found")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.RemoveUnit(unitKey);
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRemove","已处理 remove 指令");
            return new Byte[8]{0x06,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }

        /// <summary>
        /// 处理消息 0x12
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        private static Byte[] OnMessageStartAll(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStartAll","开始处理 start-all 指令");
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.StartAllUnits(false);
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStartAll","已处理 start-all 指令");
            return new Byte[8]{0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x13
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        private static Byte[] OnMessageStopAll(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStopAll","开始处理 stop-all 指令");
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.StopAllUnits();
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStopAll","已处理 stop 指令");
            return new Byte[8]{0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x14
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        private static Byte[] OnMessageRestartAll(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRestartAll","开始处理 restart-all 指令");
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.RestartAllUnits();
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRestartAll","已处理 restart-all 指令");
            return new Byte[8]{0x04,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x15
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        private static Byte[] OnMessageLoadAll(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageLoadAll","开始处理 load-all 指令");
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Int32 count=Program.UnitManageModule.LoadAllUnits();
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageLoadAll","已处理 load-all 指令");
            return new Byte[8]{0x05,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes($"loaded {count} unit")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x16
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        private static Byte[] OnMessageRemoveAll(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRemoveAll","开始处理 remove-all 指令");
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.RemoveAllUnits();
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRemoveAll","已处理 remove-all 指令");
            return new Byte[8]{0x06,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }
    }
}
