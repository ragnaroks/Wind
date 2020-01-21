using System;
using System.Collections.Generic;
using System.Text;

namespace Daemon.Entities {
    /// <summary>单元状态</summary>
    public class UnitStatus {
        /// <summary>单元名称</summary>
        public String UnitName{get;set;}
        /// <summary>单元设置</summary>
        public UnitSettings UnitSettings{get;set;}=null;
        /// <summary>单元进程</summary>
        public UnitProcess UnitProcess{get;set;}=null;
        /// <summary>单元网络情况</summary>
        public UnitNetworkCounter UnitNetworkCounter{get;set;}=null;
    }
}
