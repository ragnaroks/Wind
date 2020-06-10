using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Host.Helper {
    public class PerformanceCounters {
        /// <summary>
        /// 机器安装内存
        /// </summary>
        /// <returns></returns>
        public static Int64 GetMachineMemoryBytes() {
            PerformanceCounter counter=new PerformanceCounter("Memory","Commit Limit");
            Int64 v=(Int64)counter.NextValue();
            counter.Dispose();
            return v;
        }

        /// <summary>
        /// 机器可用内存
        /// </summary>
        /// <returns></returns>
        public static Int64 GetMachineMemoryAvailableBytes() {
            PerformanceCounter counter=new PerformanceCounter("Memory","Available Bytes");
            Int64 v=(Int64)counter.NextValue();
            counter.Dispose();
            return v;
        }

        /// <summary>
        /// 机器CPU使用率
        /// </summary>
        /// <returns></returns>
        public static Single GetProcessorUsagePercent() {
            PerformanceCounter counter=new PerformanceCounter("Processor","% Processor Time","_Total");
            Single v=counter.NextValue();
            counter.Dispose();
            return v;
        }
    }
}
