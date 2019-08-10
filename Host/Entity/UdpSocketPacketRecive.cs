using System;
using System.Collections.Generic;
using System.Text;

namespace Host.Entity {
    public class UdpSocketPacketRecive {
        public Int32 ActionId{get;set;}
        public String ActionName{get;set;}
        /// <summary>
        /// 单元名称
        /// </summary>
        public String UnitName{get;set;}=null;
    }
}
