using System;
using System.Collections.Generic;
using System.Text;

namespace Host.Entity {
    /// <summary>
    /// 单元配置
    /// </summary>
    public class UnitSettings {
        /// <summary>
        /// 单元名称
        /// </summary>
        public String name{get;set;}
        /// <summary>
        /// 应用程序绝对路径
        /// </summary>
        public String AbsolutePath{get;set;}
        /// <summary>
        /// 工作目录
        /// </summary>
        public String WorkPath{get;set;}
        /// <summary>
        /// 应用程序参数
        /// </summary>
        public String Params{get;set;}
        /// <summary>
        /// 是否自启动
        /// </summary>
        public Boolean AutoStart{get;set;}=false;
        /// <summary>
        /// 自启动延迟,单位秒
        /// </summary>
        public Int32 AutoStartDelay{get;set;}=0;
        /// <summary>
        /// 是否记录日志
        /// </summary>
        public Boolean EnableLogger{get;set;}=false;
    }
}
