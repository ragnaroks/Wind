using PeterKottas.DotNetCore.WindowsService.Interfaces;
using System;
using System.Collections.Generic;

namespace wind {
    public class DaemonService:IMicroService {
        private List<String> ExtraArguments{get;set;}=null;
        private IMicroServiceController MicroServiceController{get;set;}=null;

        public DaemonService(){}
        public DaemonService(List<String> extraArguments,IMicroServiceController microServiceController){
            this.ExtraArguments=extraArguments;
            this.MicroServiceController=microServiceController;
        }

        public void Start() {
            Helpers.LoggerModuleHelper.TryLog("DaemonService.Start[Warning]","正在启动服务");
            //启动网络监控模块
            Program.UnitNetworkCounterModule.StartTraceEventSession();
            //启动本地管理模块
            Program.LocalControlModule.StartServer();
            //启动所有单元
            Program.UnitManageModule.LoadAllUnits();
            Program.UnitManageModule.StartAllAutoUnits(true);
            Helpers.LoggerModuleHelper.TryLog("DaemonService.Start[Warning]","已启动服务");
        }

        public void Stop() {
            Helpers.LoggerModuleHelper.TryLog("DaemonService.Stop[Warning]","正在停止服务");
            //停止网络监控模块
            //Program.UnitNetworkCounterModule.Dispose();
            //停止本地管理模块
            Program.LocalControlModule.StopServer();
            //停止所有单元
            Program.UnitManageModule.StopAllUnits();
            Helpers.LoggerModuleHelper.TryLog("DaemonService.Stop[Warning]","已停止服务");
        }
    }
}
