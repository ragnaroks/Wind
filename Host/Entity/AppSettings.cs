using System;
using System.Collections.Generic;
using System.Text;

namespace Host.Entity {
    public class AppSettings{
        /// <summary>
        /// 日志等级
        /// </summary>
        public Int32 LogLevel{get;set;}
        /// <summary>
        /// 被控监听端口,UDP,WebSocket(TCP)
        /// </summary>
        public Int16 ControlPort{get;set;}
        /// <summary>
        /// 被控密钥
        /// </summary>
        public String ControlKey{get;set;}

        public override String ToString(){
            StringBuilder sb=new StringBuilder().Append("{")
                .Append("\"LogLevel\":").Append(this.LogLevel).Append(",")
                .Append("\"ControlPort\":").Append(this.ControlPort).Append(",")
                .Append("\"ControlKey\":\"").Append(this.ControlKey).Append("\"")
                .Append("}");
            return sb.ToString();
        }
    }
}
