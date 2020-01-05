using System;
using System.Diagnostics;

namespace Daemon.Entities {
    /// <summary>运行中的单元</summary>
    public class UnitProcess {
        /// <summary>单元名称</summary>
        public String Name{get;private set;}
        /// <summary>单元进程启动信息</summary>
        [Newtonsoft.Json.JsonIgnore]
        public ProcessStartInfo ProcessStartInfo{get;set;}
        /// <summary>单元进程</summary>
        [Newtonsoft.Json.JsonIgnore]
        public Process Process{get;set;}
        /// <summary>单元状态</summary>
        public Enums.UnitProcess.State State{get;set;}=Enums.UnitProcess.State.停止;
        /// <summary>单元pid</summary>
        public Int32 ProcessId{get;set;}=0;

        /// <summary>
        /// 内部设置名称
        /// </summary>
        /// <param name="name"></param>
        public void SetName(String name)=>this.Name=name;
    }
}