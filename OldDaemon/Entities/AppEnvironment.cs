using System;

namespace Daemon.Entities {
    /// <summary>应用程序环境</summary>
    public class AppEnvironment {
        /// <summary>基础目录,最后有路径分隔符</summary>
        public String BaseDirectory{get;}=AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>日志目录,最后没有路径分隔符</summary>
        public String LogsDirectory{get;}=AppDomain.CurrentDomain.BaseDirectory+"Logs";
        /// <summary>单元目录,最后没有路径分隔符</summary>
        public String UnitsDirectory{get;}=AppDomain.CurrentDomain.BaseDirectory+"Units";
        /// <summary>配置文件路径</summary>
        public String ConfigFilePath{get;}=AppDomain.CurrentDomain.BaseDirectory+"AppSettings.json";
    }
}