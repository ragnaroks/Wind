using System;
using System.Collections.Generic;
using System.Text;
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
            Program.UnitControlModule=new Modules.UnitControlModule();//读取所有单元并启动所有自启单元
            Console.WriteLine("DaemonService.Started");
            Program.LoggerModule.Log("DaemonService.Start","DaemonService Started");
        }

        /// <summary>
        /// 服务停止
        /// </summary>
        public void Stop() {
            Console.WriteLine("DaemonService.Stop");
            Program.LoggerModule.Log("DaemonService.Stop","DaemonService Stop");
            Program.UnitControlModule.Dispose();//停止所有单元并释放
            Console.WriteLine("DaemonService.Stopped");
            Program.LoggerModule.Log("DaemonService.Stop","DaemonService Stopped");
        }
    }
}
