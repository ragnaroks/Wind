using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeterKottas.DotNetCore.WindowsService.Interfaces;

namespace Daemon {
    /// <summary>
    /// 服务实例
    /// </summary>
    public class DaemonService:IMicroService{
        private readonly IMicroServiceController ServiceController;

        public DaemonService(){}
        public DaemonService(IMicroServiceController serviceController) {this.ServiceController=serviceController;}

        /// <summary>
        /// 服务启动
        /// </summary>
        public void Start() {
            Console.WriteLine("DaemonService.Start");
            Program.LoggerModule.Log("DaemonService.Start","DaemonService Start");
            Program.AppPerformanceCounterModule=new Modules.AppPerformanceCounterModule();
            Task.Factory.StartNew(()=>{
                Program.UnitNetworkPerformanceTracerModule=new Modules.UnitNetworkPerformanceTracerModule();
                if(!Program.UnitNetworkPerformanceTracerModule.Start().Result) {
                    Console.WriteLine("DaemonService.Start => UnitNetworkPerformanceTracerModule Start failed");
                }
            });
            Program.UnitControlModule=new Modules.UnitControlModule();//读取所有单元并启动所有自启单元
            if(Program.AppSettings.ControlEnable) {
                Program.ControlServerModule=new Modules.ControlServerModule(Program.AppSettings.ControlPort,Program.AppSettings.ControlAddress);
                Program.ControlServerModule.StartServer();
            }
            Console.WriteLine("DaemonService.Started");
            Program.LoggerModule.Log("DaemonService.Start","DaemonService Started");
        }

        /// <summary>
        /// 服务停止
        /// </summary>
        public void Stop() {
            Console.WriteLine("DaemonService.Stop");
            Program.LoggerModule.Log("DaemonService.Stop","DaemonService Stop");
            if(Program.AppPerformanceCounterModule!=null) {
                Program.AppPerformanceCounterModule.Dispose();
            }
            if(Program.UnitNetworkPerformanceTracerModule!=null) {
                Program.UnitNetworkPerformanceTracerModule.Dispose();
            }
            if(Program.UnitControlModule!=null){
                Program.UnitControlModule.Dispose();//停止所有单元并释放
            }
            if(Program.AppSettings.ControlEnable && Program.ControlServerModule!=null) {
                Program.ControlServerModule.Dispose();
            }
            Console.WriteLine("DaemonService.Stopped");
            Program.LoggerModule.Log("DaemonService.Stop","DaemonService Stopped");
        }
    }
}