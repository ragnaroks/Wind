using System;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using windctl.Helpers;

namespace windctl {
    public static class Program {
        /// <summary>互斥</summary>
        public static Mutex AppMutex{get;private set;}=null;
        /// <summary>应用程序环境配置</summary>
        public static Entities.Common.AppEnvironment AppEnvironment{get;}=new Entities.Common.AppEnvironment();
        /// <summary>应用程序配置</summary>
        public static Entities.Common.AppSettings AppSettings{get;}=new Entities.Common.AppSettings();
        /// <summary>日志模块</summary>
        public static Modules.LoggerModule LoggerModule{get;}=new Modules.LoggerModule();
        /// <summary>远程控制模块</summary>
        public static Modules.WebSocketControlModule RemoteControlModule{get;}=new Modules.WebSocketControlModule();
        /// <summary>是否在处理中</summary>
        public static Boolean InAction{get;set;}=false;

        static void Main(String[] args) {
            //初始化
            if(!Initialize()){
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
                Helpers.LoggerModuleHelper.TryLog("Program.Initialize[Error]","已存在实例");
                return false;
            }
            //读取配置
            if(!Directory.Exists(AppEnvironment.BaseDirectory)){return false;}
            String appSettingsFilePath=String.Concat(AppEnvironment.DataDirectory,Path.DirectorySeparatorChar,"AppSettings.json");
            if(!File.Exists(appSettingsFilePath)){return false;}
            Entities.Common.AppSettings appSettings;
            FileStream fs=null;
            try {
                fs=File.Open(appSettingsFilePath,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
                if(fs.Length<1 || fs.Length>4096){return false;}
                Span<Byte> bufferSpan=new Span<Byte>(new Byte[fs.Length]);
                fs.Read(bufferSpan);
                fs.Dispose();
                appSettings=JsonSerializer.Deserialize<Entities.Common.AppSettings>(bufferSpan);
            }catch(Exception exception){
                Helpers.LoggerModuleHelper.TryLog("Program.Initialize[Error]",$"读取应用程序配置文件异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return false;
            }finally{
                fs?.Dispose();
            }
            if(appSettings==null || String.IsNullOrWhiteSpace(appSettings.RemoteControlAddress) || String.IsNullOrWhiteSpace(appSettings.RemoteControlKey) || appSettings.RemoteControlPort<1024 || appSettings.RemoteControlPort>Int16.MaxValue){return false;}
            Regex regex=new Regex(@"^[0-9\.]{7,15}$",RegexOptions.Compiled);
            if(appSettings.RemoteControlAddress!="localhost" &&  !regex.IsMatch(appSettings.RemoteControlAddress)){return false;}
            Regex regex2=new Regex(@"^\S{32,4096}$",RegexOptions.Compiled);
            if(!regex2.IsMatch(appSettings.RemoteControlKey)){return false;}
            AppSettings.RemoteControlAddress=appSettings.RemoteControlAddress;
            AppSettings.RemoteControlPort=appSettings.RemoteControlPort;
            AppSettings.RemoteControlKey=appSettings.RemoteControlKey;
            //初始化日志模块
            if(!LoggerModule.Setup(AppEnvironment.LogsDirectory,1000)){
                LoggerModuleHelper.TryLog("Program.Initialize[Error]","初始化日志模块失败");
                return false;
            }
            //初始化控制模块
            if(!RemoteControlModule.Setup(AppSettings.RemoteControlAddress,AppSettings.RemoteControlPort,AppSettings.RemoteControlKey)) {
                LoggerModuleHelper.TryLog("Program.Initialize[Error]","初始化远程控制模块失败");
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
            //非远程控制指令
            if(!CommandHelper.IsRemoteCommand(command)){
                switch(command){
                    case "version":CommandHelper.Version();break;
                    default:CommandHelper.Help();break;
                }
                return;
            }
            //需要验证unitKey
            if(!CommandHelper.ValidUnitKey(command,argumentValue1)){
                Console.WriteLine("command execute failed,invalid unitKey");
                return;
            }
            //链接服务端
            if(!Program.RemoteControlModule.Useable){
                Console.WriteLine("command execute failed,remote control module not initialized");
                return;
            }
            if(!Program.RemoteControlModule.Start()) {
                Console.WriteLine("command execute failed,can not connect to daemon service");
                return;
            }
            if(!Program.RemoteControlModule.Valid()) {
                Console.WriteLine("command execute failed,connection invalid");
                return;
            }
            //执行远程控制指令
            switch(command){
                case "status":Program.RemoteControlModule.StatusRequest(argumentValue1);break;
                case "start":Program.RemoteControlModule.StartRequest(argumentValue1);break;
                case "stop":Program.RemoteControlModule.StopRequest(argumentValue1);break;
                case "restart":Program.RemoteControlModule.RestartRequest(argumentValue1);break;
                case "load":Program.RemoteControlModule.LoadRequest(argumentValue1);break;
                case "remove":Program.RemoteControlModule.RemoveRequest(argumentValue1);break;
                //case "attach": [1007]
                //
                //case "status-all": [1101]
                //case "start-all": [1102]
                //case "stop-all": [1103]
                //case "restart-all": [1104]//只对已启动的有效
                //case "load-all": [1105]
                //case "remove-all": [1106]
                //
                //case "daemon-version":CommandHelper.DaemonVersion();break;
                //case "daemon-status":CommandHelper.DaemonStatus();break;
                //case "daemon-shutdown":CommandHelper.DaemonShutdown();break;
                default:CommandHelper.Help();break;
            }
            //故意等待1秒
            //SpinWait.SpinUntil(()=>false,1000);
            //等待查询完成
            SpinWait.SpinUntil(()=>!InAction,8000);
        }
    }
}
