using System;
using System.Collections.Generic;
using System.Text;

namespace Host.Entity {
    public class UdpSocketPacket_Action2 {
        /// <summary>
        /// 版本
        /// </summary>
        public String HostServiceVersion{get;set;}
        /// <summary>
        /// PID
        /// </summary>
        public Int32 HostServicePid{get;set;}
        /// <summary>
        /// 线程计数
        /// </summary>
        public Int32 HostServiceThreadsCount{get;set;}
        /// <summary>
        /// 物理内存使用量,字节,包含关联进程(比如"控制台主机")
        /// </summary>
        public Int64 HostServiceMemoryUsageBytes{get;set;}
        /// <summary>
        /// 机器名
        /// </summary>
        public String MachineName{get;set;}
        /// <summary>
        /// 机器安装内存字节数
        /// </summary>
        public Int64 MachineMemoryBytes{get;set;}
        /// <summary>
        /// 机器可用内存字节数
        /// </summary>
        public Int64 MachineMemoryAvailableBytes{get;set;}
        /// <summary>
        /// 机器CPU数量
        /// </summary>
        public Int32 MachineProcessorCount{get;set;}
    }
}
