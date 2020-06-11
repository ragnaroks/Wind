using System;

namespace Daemon.Entities.Common {
    [Serializable]
    public class UnitSettings {
        /// <summary>单元名称</summary>
        public String Name{get;private set;}=null;
        /// <summary>单元描述</summary>
        public String Description{get;set;}=null;
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
        /// <summary>单元进程类型,0:默认,1:fork子进程</summary>
        public Int32 Type{get;set;}=0;
        /// <summary>是否获取网络使用数据,0:关闭,1:仅下载,2:仅上传,3:全部</summary>
        public Int32 MonitorNetworkUsage{get;set;}=0;
    }
}
