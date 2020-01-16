using System;
using System.Collections.Generic;
using System.Text;

namespace Daemon.Entities {
    public class WebSocketConnectionWrap{
        public Nullable<Guid> WebSocketConnectionId=null;
        public Fleck.IWebSocketConnection WebSocketConnection=null;
        public Boolean Valid=false;
        public Int64 LastPongTime=0;
        public Int32 ReceivedByteSize=0;
        public Int32 SentByteSize=0;
    }
}
