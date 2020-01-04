using System;

namespace Daemon.Entities {
    /// <summary>单元配置</summary>
    public class UnitSettings {
        /// <summary>单元名称</summary>
        public String Name{get;private set;}
        /// <summary>单元描述</summary>
        public String Description{get;set;}
        /// <summary>单元可执行文件绝对路径</summary>
        public String ExecuteAbsolutePath{get;set;}
        /// <summary>单元工作绝对目录</summary>
        public String WorkAbsoluteDirectory{get;set;}
        /// <summary>可执行文件参数</summary>
        public String ExecuteParams{get;set;}
        /// <summary>单元是否自启,默认不自启</summary>
        public Boolean AutoStart{get;set;}=false;
        /// <summary>单元自启延迟(秒),默认10秒</summary>
        public Int32 AutoStartDelay{get;set;}=10;

        /// <summary>
        /// 内部设置单元名称
        /// </summary>
        /// <param name="name"></param>
        public void SetName(String name)=>this.Name=name;
    }
}