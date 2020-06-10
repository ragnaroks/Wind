using System;
using System.Collections.Generic;
using System.Text;

namespace Daemon.Entities.Common {
    public class Unit {
        /// <summary>单元名称(内部标识用)</summary>
        public String UnitName{get;set;}
        /// <summary>单元配置</summary>
        public UnitSettings UnitSettings{get;set;}
        /// <summary>单元状态</summary>
        public UnitStatus UnitStatus{get;set;}
    }
}
