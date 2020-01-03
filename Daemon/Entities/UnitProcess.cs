using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Daemon.Entities {
    /// <summary>运行中的单元</summary>
    public class UnitProcess {
        /// <summary>单元名称</summary>
        public String Name{get;private set;}
        /// <summary>单元进程启动信息</summary>
        public ProcessStartInfo ProcessStartInfo{get;set;}
        /// <summary>单元进程</summary>
        public Process Process{get;set;}
        /// <summary>单元状态</summary>
        public Enums.UnitProcess.State State{get;set;}=Enums.UnitProcess.State.停止;

        public void SetName(String name)=>this.Name=name;
    }
}
