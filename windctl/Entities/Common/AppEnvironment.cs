using System;
using System.Text.Json;

namespace windctl.Entities.Common {
    /// <summary>应用程序环境配置</summary>
    public class AppEnvironment {
        /// <summary>Json序列号规则</summary>
        public JsonSerializerOptions JsonSerializerOptions{get;}=new JsonSerializerOptions{
            PropertyNamingPolicy=null,IgnoreNullValues=false,PropertyNameCaseInsensitive=false,Encoder=System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping};
        /// <summary>物理根路径,有路径分隔符</summary>
        public String BaseDirectory{get;}=AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>日志根路径,无路径分隔符</summary>
        public String LogsDirectory{get;}=AppDomain.CurrentDomain.BaseDirectory+"Logs";
        /// <summary>数据根路径,无路径分隔符</summary>
        public String DataDirectory{get;}=AppDomain.CurrentDomain.BaseDirectory+"Data";
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
