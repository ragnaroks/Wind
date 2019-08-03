using PeterKottas.DotNetCore.WindowsService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Host {
    public class HostService:IMicroService {
        private IMicroServiceController controller;

        public HostService(){this.controller=null;}
        public HostService(IMicroServiceController _controller) {this.controller=_controller;}

        public void Start() {
            //var a=Function.AesEncrypt.Encrypt(Program.AppSettings.ControlKey,"1234abcdABCD周吴郑王");
            //var b=Function.AesEncrypt.Decrypt(Program.AppSettings.ControlKey,a).TrimEnd('\0');
            Program.Logger.Log("HostService","Start");
        }

        public void Stop() {
            Program.Logger.Log("HostService","Stop");
        }
    }
}
