using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeterKottas.DotNetCore.WindowsService.Interfaces;

namespace Daemon {
    /// <summary>服务实例</summary>
    public class DaemonService:IMicroService{
        private IMicroServiceController ServiceController{get;}

        public DaemonService(){}
        public DaemonService(IMicroServiceController serviceController) {this.ServiceController=serviceController;}

        /// <summary>
        /// 服务启动
        /// </summary>
        public void Start() {
            Console.WriteLine("DaemonService.Start");
            Program.LoggerModule.Log("DaemonService.Start","DaemonService Start");
            //初始化应用程序性能计数器模块
            Program.SetAppPerformanceCounterModule(new Modules.AppPerformanceCounterModule());
            //初始化单元网络统计模块
            Program.SetUnitNetworkPerformanceTracerModule(new Modules.UnitNetworkPerformanceTracerModule());
            Task.Run(()=>{
                if(!Program.UnitNetworkPerformanceTracerModule.StartProcess()) {
                    Console.WriteLine("DaemonService.Start => UnitNetworkPerformanceTracerModule Start failed");
                }
            });
            //初始化远程控制模块
            if(Program.AppSettingsModule.AppSettings.ControlEnable){
                Program.ControlServerModule.StartServer();
            }
            //读取所有单元并启动所有自启单元
            if(!Program.UnitControlModule.LoadAllUnits()) {
                Console.WriteLine("DaemonService.Start => 没有正常读取到单元列表");
                Program.LoggerModule.Log("DaemonService.Start","没有正常读取到单元列表");
            } else {
                Program.UnitControlModule.StartAllAutoStartUnits();
            }
            //过程完成
            Console.WriteLine("DaemonService.Started");
            Program.LoggerModule.Log("DaemonService.Start","DaemonService Started");
        }

        /// <summary>
        /// 服务停止
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization","CA1303:请不要将文本作为本地化参数传递",Justification = "<挂起>")]
        public void Stop() {
            Console.WriteLine("DaemonService.Stop");
            Program.LoggerModule.Log("DaemonService.Stop","DaemonService Stop");
            //停止应用程序性能计数器
            if(Program.AppPerformanceCounterModule!=null) {
                Program.AppPerformanceCounterModule.Dispose();
            }
            //停止单元网络统计
            if(Program.UnitNetworkPerformanceTracerModule!=null) {
                Program.UnitNetworkPerformanceTracerModule.Dispose();
            }
            //停止所有单元并释放
            if(Program.UnitControlModule!=null){
                Program.UnitControlModule.StopAllUnits();
                Program.UnitControlModule.Dispose();
            }
            //停止远程控制
            if(Program.AppSettingsModule.AppSettings.ControlEnable && Program.ControlServerModule.ModuleAvailable){
                Program.ControlServerModule.Dispose();
            }
            //过程完成
            Console.WriteLine("DaemonService.Stopped");
            Program.LoggerModule.Log("DaemonService.Stop","DaemonService Stopped");
        }
    }
}