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
            //启动所有单元
            Program.UnitManageModule.StopAllUnits();
        }

        public void Stop() {
            //停止所有单元
            Program.UnitManageModule.StopAllUnits();
        }
    }
}
