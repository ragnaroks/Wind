using System;
using System.Text.Json;

namespace DaemonController.Entities.Common {
    /// <summary>应用程序环境配置</summary>
    public class AppEnvironment {
        /// <summary>物理根路径,有路径分隔符</summary>
        public String BaseDirectory{get;}=AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>命名管道</summary>
        public String PipelineName{get;}="WIND2_DAEMON_PIPELINE";
        #if DEBUG
        /// <summary>是否开发模式</summary>
        public Boolean DevelopMode{get;}=true;
        #else
        /// <summary>是否开发模式</summary>
        public Boolean DevelopMode{get;}=false;
        #endif
        
        public override String ToString()=>JsonSerializer.Serialize(this);
    }
}
