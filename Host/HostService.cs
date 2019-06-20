using PeterKottas.DotNetCore.WindowsService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Host {
    class HostService:IMicroService {
        public void Start() {
            Program.Logger.Log("服务正在启动");

            var a=Function.AesEncrypt.Encrypt(Program.AppSettings.ControlKey,"1234abcdABCD周吴郑王");
            var b=Function.AesEncrypt.Decrypt(Program.AppSettings.ControlKey,a).TrimEnd('\0');
            
            Program.Logger.Log("服务已启动");
            this.Stop();
        }

        public void Stop() {
            Program.Logger.Log("服务正在停止");
            Program.Logger.Log("服务已停止");
        }
    }
}
