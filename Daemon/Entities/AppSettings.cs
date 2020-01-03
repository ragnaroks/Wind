using System;
using System.Collections.Generic;
using System.Text;

namespace Daemon.Entities {
    /// <summary>应用程序配置</summary>
    public class AppSettings {
        /// <summary>是否启用远程控制</summary>
        public Boolean ControlEnable{get;set;}=false;
        /// <summary>
        /// 远控监听地址
        /// "any"="0.0.0.0"
        /// "localhost"="127.0.0.1"
        /// </summary>
        public String ControlAddressV4{get;set;}="localhost";
        /// <summary>
        /// 远控监听地址
        /// "any"="::"
        /// "localhost"="::1"
        /// </summary>
        public String ControlAddressV6{get;set;}="localhost";
        /// <summary>
        /// 远控监听端口
        /// </summary>
        public Int16 ControlPort{get;set;}=25565;
        /// <summary>
        /// 远控加解密key
        /// </summary>
        public String ControlKey{get;set;}="https://github.com/ragnaroks/Wind2";
    }
}
