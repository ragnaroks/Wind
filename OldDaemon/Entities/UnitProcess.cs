using System;
using System.Diagnostics;

namespace Daemon.Entities {
    /// <summary>运行中的单元</summary>
    public class UnitProcess {
        /// <summary>单元名称</summary>
        public String Name{get;private set;}
        /// <summary>单元进程启动信息</summary>
        public ProcessStartInfo ProcessStartInfo{get;set;}
        /// <summary>单元进程</summary>
        public Process Process{get;set;}
        /// <summary>单元状态 停止=1,正在启动=2,运行中=3,正在停止=4</summary>
        public Int32 State{get;set;}=1;
        /// <summary>单元pid</summary>
        public Int32 ProcessId{get;set;}=0;

        /// <summary>
        /// 内部设置名称
        /// </summary>
        /// <param name="name"></param>
        public void SetName(String name)=>this.Name=name;
    }
}