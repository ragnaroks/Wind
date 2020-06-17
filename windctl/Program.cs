using System;
using System.IO;
using System.Threading;
using windctl.Helpers;

namespace windctl {
    public static class Program {
        /// <summary>互斥</summary>
        public static Mutex AppMutex{get;private set;}=null;
        /// <summary>应用程序环境配置</summary>
        public static Entities.Common.AppEnvironment AppEnvironment{get;}=new Entities.Common.AppEnvironment();
        /// <summary>日志模块</summary>
        public static Modules.LoggerModule LoggerModule{get;}=new Modules.LoggerModule();

        static void Main(String[] args) {
            //初始化
            if(Initialize()){
                Environment.Exit(0);
                return;
            }
            //默认指令
            if(args.GetLength(0)<1){
                CommandHelper.Help();
                return;
            }
            //执行指令
            InvokeCommand(args);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        private static Boolean Initialize() {
            //互斥
            AppMutex=new Mutex(true,"WindCommandLineController",out Boolean mutex);
            if(!mutex){
                Helpers.LoggerModuleHelper.TryLog("Program.Main[Error]","已存在实例");
                return false;
            }
            //初始化日志模块
            if(!LoggerModule.Setup(AppEnvironment.LogsDirectory,1000)){
                LoggerModuleHelper.TryLog("Program.Main[Error]","初始化日志模块失败");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 执行指令
        /// </summary>
        /// <param name="args"></param>
        private static void InvokeCommand(String[] args){
            String command=args[0];
            String argumentValue1=args.GetLength(0)>1?args[1]:null;
            //String argumentValue2=args.GetLength(0)>2?args[2]:null;
            switch(command){
                //case "status": [1001]
                //case "start": [1002]
                //case "stop": [1003]
                //case "restart": [1004]//只对已启动的有效
                //case "load": [1005]
                //case "remove": [1006]
                //case "attach": [1007]
                //
                //case "status-all": [1011]
                //case "start-all": [1012]
                //case "stop-all": [1013]
                //case "restart-all": [1014]//只对已启动的有效
                //case "load-all": [1015]
                //case "remove-all": [1016]
                //
                //case "daemon-status": [1021]
                //case "daemon-version": [1027]
                //case "daemon-shutdown": [1028]
                //
                case "version":CommandHelper.Version();return;
                default:CommandHelper.Help();return;
            }
        }
    }
}
