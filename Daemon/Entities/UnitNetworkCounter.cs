using System;
using System.Collections.Generic;
using System.Text;

namespace Daemon.Entities {
    public class UnitNetworkCounter{
        public Int64 TotalSent=0;
        public Int64 TotalReceived=0;
        public Int64 LastCulTotalSent=0;
        public Int64 LastCulTotalReceived=0;
        public Int64 SendSpeed=0;
        public Int64 ReceiveSpeed=0;
    }
}
