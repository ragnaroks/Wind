using System;

namespace wind.Entities.Common {
    [Serializable]
    public class UnitSettings {
        /// <summary>单元名称</summary>
        public String Name{get;set;}=null;
        /// <summary>单元描述</summary>
        public String Description{get;set;}=null;
        /// <summary>单元进程类型,0:默认,1:fork子进程</summary>
        public Int32 Type{get;set;}=0;
        /// <summary>单元可执行文件绝对路径</summary>
        public String AbsoluteExecutePath{get;set;}=null;
        /// <summary>单元工作绝对目录</summary>
        public String AbsoluteWorkDirectory{get;set;}=null;
        /// <summary>可执行文件参数</summary>
        public String Arguments{get;set;}=null;
        /// <summary>单元是否自启,默认不自启</summary>
        public Boolean AutoStart{get;set;}=false;
        /// <summary>单元自启延迟(秒),默认10秒</summary>
        public Int32 AutoStartDelay{get;set;}=10;
        /// <summary>守护进程</summary>
        public Boolean RestartWhenException{get;set;}=false;
        /// <summary>进程优先级</summary>
        public String PriorityClass{get;set;}=null;
        /// <summary>CPU亲和性</summary>
        public String ProcessorAffinity{get;set;}=null;
        /// <summary>输入编码</summary>
        public String StandardInputEncoding{get;set;}=null;
        /// <summary>输出编码</summary>
        public String StandardOutputEncoding{get;set;}=null;
        /// <summary>错误输出编码</summary>
        public String StandardErrorEncoding{get;set;}=null;
        /// <summary>是否获取性能使用数据</summary>
        public Boolean MonitorPerformanceUsage{get;set;}=false;
        /// <summary>是否获取网络使用数据</summary>
        public Boolean MonitorNetworkUsage{get;set;}=false;
    }
}
