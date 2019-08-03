using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Host.Entity {
    public class Settings {
        /// <summary>
        /// 程序集目录,最后没有路径分隔符
        /// </summary>
        public String CurrentDirectory=Environment.CurrentDirectory;
        /// <summary>
        /// 日志存放目录,最后没有路径分隔符
        /// </summary>
        public String LogDirectory=Environment.CurrentDirectory+Path.DirectorySeparatorChar+"Logs";
        /// <summary>
        /// 是否暂停
        /// </summary>
        public Boolean IsPaused=false;
    }
}
