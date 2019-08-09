using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Host.Entity {
    public class UdpSocketRemote {
        public IPEndPoint IPEndPoint{get;set;}
        public Int64 LastUpdateTime{get;set;}
    }
}
