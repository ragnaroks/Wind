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
        /// <summary>已附加到的单元</summary>
        public static String AttachedUnitKey{get;set;}=null;

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
                Helpers.LoggerModuleHelper.TryLog("Program.Initialize[Error]",$"读取应用程序配置文件异常,{exception.Message}\n异常堆栈: {exception.StackTrace}");
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
            //
            Console.CancelKeyPress+=ConsoleCancelKeyPress;
            return true;
        }

        private static void ConsoleCancelKeyPress(object sender,ConsoleCancelEventArgs consoleCancelEventArgs) {
            //取消此控制台的^c指令
            consoleCancelEventArgs.Cancel=true;
            //并转发到已附加的单元
            if(String.IsNullOrWhiteSpace(AttachedUnitKey)){return;}
            RemoteControlModule.CommandlineRequest(AttachedUnitKey,9,String.Empty);
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
            if(!CommandHelper.RequireUnitKey(command,argumentValue1)){
                Console.WriteLine("command execute failed,invalid unitKey");
                return;
            }
            //链接服务端
            if(!RemoteControlModule.Useable){
                Console.WriteLine("command execute failed,remote control module not initialized");
                return;
            }
            if(!RemoteControlModule.Start()) {
                Console.WriteLine("command execute failed,can not connect to daemon service");
                return;
            }
            if(!RemoteControlModule.Valid()) {
                Console.WriteLine("command execute failed,connection invalid");
                return;
            }
            //执行远程控制指令
            switch(command){
                case "status":RemoteControlModule.StatusRequest(argumentValue1);break;
                case "start":RemoteControlModule.StartRequest(argumentValue1);break;
                case "stop":RemoteControlModule.StopRequest(argumentValue1);break;
                case "restart":RemoteControlModule.RestartRequest(argumentValue1);break;
                case "load":RemoteControlModule.LoadRequest(argumentValue1);break;
                case "remove":RemoteControlModule.RemoveRequest(argumentValue1);break;
                case "logs":RemoteControlModule.LogsRequest(argumentValue1);break;
                case "attach":
                    AttachedUnitKey=argumentValue1;
                    RemoteControlModule.LogsRequest(AttachedUnitKey);
                    break;
                case "status-all":RemoteControlModule.StatusAllRequest();break;
                case "start-all":RemoteControlModule.StartAllRequest();break;
                case "stop-all":RemoteControlModule.StopAllRequest();break;
                case "restart-all":RemoteControlModule.RestartAllRequest();break;
                case "load-all":RemoteControlModule.LoadAllRequest();break;
                case "remove-all":RemoteControlModule.RemoveAllRequest();break;
                case "daemon-version":RemoteControlModule.DaemonVersionRequest();break;
                case "daemon-status":RemoteControlModule.DaemonStatusRequest();break;
                case "daemon-shutdown":RemoteControlModule.DaemonShutdownRequest();break;
                default:CommandHelper.Help();break;
            }
            //等待查询完成
            if(command=="attach"){
                SpinWait.SpinUntil(()=>false,1000);
                while(!String.IsNullOrWhiteSpace(AttachedUnitKey)){
                    String attachedCommandLine=Console.ReadLine();
                    if(String.IsNullOrWhiteSpace(attachedCommandLine)){continue;}
                    RemoteControlModule.CommandlineRequest(AttachedUnitKey,1,attachedCommandLine.Trim());
                }
            } else {
                SpinWait.SpinUntil(()=>!InAction,8000);
            }
        }
    }
}
