using Fleck;
using System;
using System.Collections.Generic;
using System.Text;

namespace wind.Entities.Common {
    public class ClientConnection {
        /// <summary>GUID</summary>
        public String Id{get;set;}=null;
        /// <summary>是否已验证</summary>
        public Boolean Valid{get;set;}=false;
        /// <summary>最后在线时间</summary>
        public Int64 LastOnlineTime{get;set;}=0;
        /// <summary>是否支持通知</summary>
        public Boolean SupportNotify{get;set;}=false;
        /// <summary>链接</summary>
        public IWebSocketConnection WebSocketConnection{get;set;}=null;
    }
}
