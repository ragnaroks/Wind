using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Host {
    class Program {
        /// <summary>
        /// 程序集目录,最后附加路径分隔符
        /// </summary>
        internal static String CurrentDirectory=Environment.CurrentDirectory;
        /// <summary>
        /// 日志存放目录
        /// </summary>
        internal static String LogDirectory=Environment.CurrentDirectory+Path.DirectorySeparatorChar+"Logs";
        /// <summary>
        /// 配置
        /// </summary>
        internal static Entity.AppSettings AppSettings;
        /// <summary>
        /// 服务
        /// </summary>
        internal static HostService Service=null;
        /// <summary>
        /// 日志
        /// </summary>
        internal static Helper.Logger Logger=null;

        static void Main(String[] _args){
            //配置
            if(!File.Exists(CurrentDirectory+Path.DirectorySeparatorChar+"AppSettings.json")){throw new Exception("未找到配置文件");}
            try {
                FileStream fs1=File.Open(CurrentDirectory+Path.DirectorySeparatorChar+"AppSettings.json",FileMode.Open,FileAccess.Read,FileShare.Read);
                if(fs1.Length>Int32.MaxValue){throw new Exception("配置文件异常");}
                Byte[] buffer=new Byte[fs1.Length];
                fs1.Read(buffer,0,(Int32)fs1.Length);
                fs1.Dispose();
                String JSON=System.Text.Encoding.UTF8.GetString(buffer);
                AppSettings=Newtonsoft.Json.JsonConvert.DeserializeObject<Entity.AppSettings>(JSON);
            }catch(Exception _e){
                //AppSettings=new Entity.AppSettings();
                throw _e;
            }
            //日志
            Logger=new Helper.Logger(LogDirectory);
            //服务
            Service=new HostService();
            Service.Start();
        }
    }
}
