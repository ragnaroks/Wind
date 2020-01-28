using System;
using System.Collections.Generic;
using System.Text;

namespace Daemon.Entities {
    public class WebSocketConnectionWrap{
        public Nullable<Guid> WebSocketConnectionId{get;set;}=null;
        public Fleck.IWebSocketConnection WebSocketConnection{get;set;}=null;
        public Boolean Valid{get;set;}=false;
        public Int64 LastPongTime{get;set;}=0;
        public Int32 ReceivedByteSize{get;set;}=0;
        public Int32 SentByteSize{get;set;}=0;
    }
}
