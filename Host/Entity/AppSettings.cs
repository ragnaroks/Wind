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
        /// 是否启用被控
        /// </summary>
        public Boolean ControlEnable{get;set;}
        /// <summary>
        /// 被控监听地址
        /// </summary>
        public String ControlAddress{get;set;}
        /// <summary>
        /// 被控监听端口,UDP,WebSocket(TCP)
        /// </summary>
        public UInt16 ControlPort{get;set;}
        /// <summary>
        /// 被控密钥
        /// </summary>
        public String ControlKey{get;set;}

        public override String ToString(){
            StringBuilder sb=new StringBuilder().Append("{")
                .Append("\"LogLevel\":").Append(this.LogLevel).Append(",")
                .Append("\"ControlEnable\":").Append(this.ControlEnable?"true":"false").Append(",")
                .Append("\"ControlAddress\":\"").Append(this.ControlAddress).Append("\",")
                .Append("\"ControlPort\":").Append(this.ControlPort).Append(",")
                .Append("\"ControlKey\":\"").Append(this.ControlKey).Append("\"")
                .Append("}");
            return sb.ToString();
        }
    }
}
