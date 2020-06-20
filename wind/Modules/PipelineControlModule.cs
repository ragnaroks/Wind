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
