using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Host.Entity {
    public class Unit {
        /// <summary>
        /// 状态,0:stop,1:start,2:running
        /// </summary>
        public Int32 State{get;set;}=0;
        /// <summary>
        /// 单元配置
        /// </summary>
        public UnitSettings UnitSettings{get;set;}
        /// <summary>
        /// 单元进程
        /// </summary>
        public Process Process{get;set;}
    }
}
