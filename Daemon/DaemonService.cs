using PeterKottas.DotNetCore.WindowsService.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Daemon {
    public class DaemonService:IMicroService {
        private List<String> ExtraArguments{get;set;}=null;
        private IMicroServiceController MicroServiceController{get;set;}=null;

        public DaemonService(){}
        public DaemonService(List<String> extraArguments,IMicroServiceController microServiceController){
            this.ExtraArguments=extraArguments;
            this.MicroServiceController=microServiceController;
        }

        public void Start() {
            //启动本地管理模块
            Program.LocalControlModule.StartServer();
            //启动所有单元
            //Program.UnitManageModule.StartAllUnitsAsync(true).Wait();
        }

        public void Stop() {
            //停止本地管理模块
            Program.LocalControlModule.StopServer();
            //停止所有单元
            //Program.UnitManageModule.StopAllUnitsAsync().Wait();
        }
    }
}
