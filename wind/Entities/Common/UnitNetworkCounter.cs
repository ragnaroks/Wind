using System;

namespace wind.Entities.Common {
    public class UnitNetworkCounter{
        public Int64 TotalSent{get;set;}=0;
        public Int64 TotalReceived{get;set;}=0;
        public Int64 LastCulTotalSent{get;set;}=0;
        public Int64 LastCulTotalReceived{get;set;}=0;
        public Int64 SendSpeed{get;set;}=0;
        public Int64 ReceiveSpeed{get;set;}=0;
    }
}
