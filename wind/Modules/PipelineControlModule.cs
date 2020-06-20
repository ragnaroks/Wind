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
    [Obsolete]
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
    }
}
