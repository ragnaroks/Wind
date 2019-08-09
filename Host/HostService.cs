using Host.Helper;
using PeterKottas.DotNetCore.WindowsService.Interfaces;

namespace Host {
    public class HostService:IMicroService {
        private IMicroServiceController controller;
        
        public HostService(){this.controller=null;}
        public HostService(IMicroServiceController _controller) {this.controller=_controller;}

        public void Start() {
            Program.Logger.Log("HostService","Start");
            UnitControl.RefreshUnits();
            UnitControl.StartAllUnits(true);
            Program.Logger.Log("HostService","Started");
        }

        public void Stop() {
            Program.Logger.Log("HostService","Stop");
            UnitControl.StopAllUnits();
            Program.Logger.Log("HostService","Stopped");
        }
    }
}
