using Daemon.Modules;
using PeterKottas.DotNetCore.WindowsService;
using PeterKottas.DotNetCore.WindowsService.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;

namespace Daemon {
    public static class Program {
        /// <summary>互斥</summary>
        public static Mutex AppMutex{get;private set;}=null;
        /// <summary>应用程序进程</summary>
        public static Process AppProcess{get;private set;}=null;
        /// <summary>服务句柄</summary>
        public static IMicroServiceController DaemonServiceController{get;private set;}=null;
        /// <summary>应用程序环境配置</summary>
        public static Entities.Common.AppEnvironment AppEnvironment{get;}=new Entities.Common.AppEnvironment();
        /// <summary>日志模块 01</summary>
        public static Modules.LoggerModule LoggerModule{get;}=new Modules.LoggerModule();
        /// <summary>应用程序配置 02</summary>
        public static Entities.Common.AppSettings AppSettings{get;}=new Entities.Common.AppSettings();
        /// <summary>单元性能监控模块 03</summary>
        public static Modules.UnitPerformanceCounterModule UnitPerformanceCounterModule{get;}=new Modules.UnitPerformanceCounterModule();
        /// <summary>单元网络数据监控模块 04</summary>
        public static Modules.UnitNetworkCounterModule UnitNetworkCounterModule{get;}=new UnitNetworkCounterModule();
        /// <summary>本地管理模块 05</summary>
        public static Modules.PipelineControlModule LocalControlModule{get;}=new Modules.PipelineControlModule();
        /// <summary>远程管理模块 06</summary>
        public static Modules.WebSocketControlModule RemoteControlModule{get;}=new Modules.WebSocketControlModule();
        /// <summary>单元管理模块 07</summary>
        public static Modules.UnitManageModule UnitManageModule{get;}=new Modules.UnitManageModule();
        
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        static void Main(String[] args) {
            Helpers.LoggerModuleHelper.TryLog("Program.Main",$"应用程序参数: {JsonSerializer.Serialize(args)}");
            AppDomain.CurrentDomain.ProcessExit+=CurrentDomainProcessExit;
            AppDomain.CurrentDomain.UnhandledException+=CurrentDomainUnhandledException;
            AppProcess=Process.GetCurrentProcess();
            Program.AppMutex=new Mutex(true,"WindDaemonAppMutex",out Boolean mutex);
            if(!mutex){
                Helpers.LoggerModuleHelper.TryLog("Program.Main[Error]","已存在实例");
                Environment.Exit(0);
                return;
            }
            if(!InitializeLoggerModule()) {
                Helpers.LoggerModuleHelper.TryLog("Program.Main[Error]","初始化日志模块失败");
                Environment.Exit(0);
                return;
            }
            if(!InitializeAppSettings()) {
                Helpers.LoggerModuleHelper.TryLog("Program.Main[Error]","读取应用程序配置失败");
                Environment.Exit(0);
                return;
            }
            if(!InitializeUnitPerformanceCounterModule()){
                Helpers.LoggerModuleHelper.TryLog("Program.Main[Error]","初始化单元性能监控模块失败");
                Environment.Exit(0);
                return;
            }
            if(!InitializeUnitNetworkCounterModule()){
                Helpers.LoggerModuleHelper.TryLog("Program.Main[Error]","初始化单元网络监控模块失败");
                Environment.Exit(0);
                return;
            }
            if(!InitializeLocalControlModule()) {
                Helpers.LoggerModuleHelper.TryLog("Program.Main[Error]","初始化本地管理模块失败");
                Environment.Exit(0);
                return;
            }
            if(!InitializeUnitManageModule()) {
                Helpers.LoggerModuleHelper.TryLog("Program.Main[Error]","初始化单元管理模块失败");
                Environment.Exit(0);
                return;
            }
            Helpers.LoggerModuleHelper.TryLog("Program.Main",$"服务结果: {ServiceRun()}");
        }

        /// <summary>
        /// 初始化日志模块
        /// </summary>
        /// <returns>是否成功</returns>
        private static Boolean InitializeLoggerModule(){
            if(!Directory.Exists(AppEnvironment.LogsDirectory)) {
                try {
                    _=Directory.CreateDirectory(AppEnvironment.LogsDirectory);
                } catch(Exception exception) {
                    Helpers.LoggerModuleHelper.TryLog("Program.InitializeLoggerModule[Error]",$"创建日志目录异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                    return false;
                }
            }
            return LoggerModule.Setup(AppEnvironment.LogsDirectory,1000);
        }

        /// <summary>
        /// 读取应用程序配置
        /// </summary>
        /// <returns>是否成功</returns>
        private static Boolean InitializeAppSettings(){
            if(!Directory.Exists(AppEnvironment.BaseDirectory)){return false;}
            String appSettingsFilePath=String.Concat(AppEnvironment.DataDirectory,Path.DirectorySeparatorChar,"AppSettings.json");
            if(!File.Exists(appSettingsFilePath)){return false;}
            Entities.Common.AppSettings appSettings;
            //读取文件并反序列化
            FileStream fs=null;
            try {
                fs=File.Open(appSettingsFilePath,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
                if(fs.Length<1 || fs.Length>4096){return false;}
                Span<Byte> bufferSpan=new Span<Byte>(new Byte[fs.Length]);
                fs.Read(bufferSpan);
                fs.Dispose();
                appSettings=JsonSerializer.Deserialize<Entities.Common.AppSettings>(bufferSpan);
            }catch(Exception exception){
                Helpers.LoggerModuleHelper.TryLog("Program.InitializeAppSettings[Error]",$"读取应用程序配置文件异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return false;
            }finally{
                fs?.Dispose();
            }
            //检查
            if(appSettings==null || String.IsNullOrWhiteSpace(appSettings.RemoteControlListenAddress) || String.IsNullOrWhiteSpace(appSettings.RemoteControlKey) || appSettings.RemoteControlListenPort<1024 || appSettings.RemoteControlListenPort>Int16.MaxValue){return false;}
            Regex regex=new Regex(@"^[0-9\.]{7,15}$",RegexOptions.Compiled);
            if(appSettings.RemoteControlListenAddress!="localhost" &&  !regex.IsMatch(appSettings.RemoteControlListenAddress)){return false;}
            Regex regex2=new Regex(@"^\S{8,128}$",RegexOptions.Compiled);
            if(!regex2.IsMatch(appSettings.RemoteControlKey)){return false;}
            //完成
            AppSettings.EnableRemoteControl=appSettings.EnableRemoteControl;
            AppSettings.RemoteControlListenAddress=appSettings.RemoteControlListenAddress;
            AppSettings.RemoteControlListenPort=appSettings.RemoteControlListenPort;
            AppSettings.RemoteControlKey=appSettings.RemoteControlKey;
            return true;
        }

        /// <summary>
        /// 初始化单元管理模块
        /// </summary>
        /// <returns>是否成功</returns>
        private static Boolean InitializeUnitManageModule(){
            if(!Directory.Exists(AppEnvironment.UnitsDirectory)) {
                try {
                    _=Directory.CreateDirectory(AppEnvironment.UnitsDirectory);
                } catch(Exception exception) {
                    Helpers.LoggerModuleHelper.TryLog("Program.InitializeUnitManageModule[Error]",$"创建单元存放目录异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                    return false;
                }
            }
            return UnitManageModule.Setup(AppEnvironment.UnitsDirectory);
        }

        /// <summary>
        /// 初始化单元性能监控模块
        /// </summary>
        /// <returns>是否成功</returns>
        private static Boolean InitializeUnitPerformanceCounterModule(){
            if(!UnitPerformanceCounterModule.Setup()){return false;}
            UnitPerformanceCounterModule.Add(AppProcess.Id);
            return true;
        }

        /// <summary>
        /// 初始化单元性能监控模块
        /// </summary>
        /// <returns>是否成功</returns>
        private static Boolean InitializeUnitNetworkCounterModule(){
            if(!UnitNetworkCounterModule.Setup()){return false;}
            UnitNetworkCounterModule.Add(AppProcess.Id);
            return true;
        }

        /// <summary>
        /// 初始化本地控制模块
        /// </summary>
        /// <returns>是否成功</returns>
        private static Boolean InitializeLocalControlModule(){
            if(String.IsNullOrWhiteSpace(AppEnvironment.PipelineName)){return false;}
            return LocalControlModule.Setup(AppEnvironment.PipelineName);
        }

        /// <summary>
        /// 应用程序未处理异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomainUnhandledException(object sender,UnhandledExceptionEventArgs e) {
            Helpers.LoggerModuleHelper.TryLog("Program.CurrentDomainUnhandledException[Error]",$"服务主机未处理异常\n{e.ExceptionObject}");
        }

        /// <summary>
        /// 应用程序退出之前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomainProcessExit(object sender,EventArgs e){
            //释放远程管理模块
            RemoteControlModule.Dispose();
            //释放本地管理模块
            LocalControlModule.Dispose();
            //释放单元管理模块,应确保已无单元正在运行
            UnitManageModule.Dispose();
            //释放单元性能监控模块
            UnitPerformanceCounterModule.Dispose();
            //是否单元网络监控模块
            UnitNetworkCounterModule.Dispose();
            //停止服务
            if(DaemonServiceController!=null){ DaemonServiceController.Stop(); }
            //释放自身进程引用
            AppMutex.Dispose();
            AppProcess.Dispose();
            //释放日志模块
            Helpers.LoggerModuleHelper.TryLog("Program.CurrentDomainProcessExit[Warning]","服务主机进程退出");
            LoggerModule.Dispose();
        }

        /// <summary>
        /// 运行服务
        /// </summary>
        /// <returns>运行结果</returns>
        private static Int32 ServiceRun() {
            return ServiceRunner<DaemonService>.Run(config=>{
                config.SetDisplayName("Wind");
                config.SetName("Wind");
                config.SetDescription("Wind 服务主机");
                config.SetConsoleTimeout(1000);
                config.SetServiceTimeout(6000);
                config.Service(serviceConfigurator=>{
                    serviceConfigurator.ServiceFactory((extraArguments,microServiceController)=>{
                        DaemonServiceController=microServiceController;
                        return new DaemonService(extraArguments,microServiceController);
                    });
                    //安装
                    serviceConfigurator.OnInstall(server=>{
                        Helpers.LoggerModuleHelper.TryLog("Program.ServiceRun[Warning]","已安装 Wind 服务主机");
                    });
                    //卸载
                    serviceConfigurator.OnUnInstall(service=>{
                        Helpers.LoggerModuleHelper.TryLog("Program.ServiceRun[Warning]","已卸载 Wind 服务主机");
                    });
                    /*
                    //继续
                    serviceConfigurator.OnContinue(server=>{
                        Helpers.LoggerModuleHelper.TryLog("Program.ServiceRun[Warning]","已恢复运行 Wind 服务主机");
                    });
                    //暂停
                    serviceConfigurator.OnPause(server=>{
                        Helpers.LoggerModuleHelper.TryLog("Program.ServiceRun[Warning]","已暂停运行 Wind 服务主机");
                    });
                    */
                    //退出
                    serviceConfigurator.OnShutdown(service=>{
                        service.Stop();
                        Helpers.LoggerModuleHelper.TryLog("Program.ServiceRun[Warning]","Wind 服务主机宿主正在关闭");
                    });
                    //错误
                    serviceConfigurator.OnError(exception=>{
                        Helpers.LoggerModuleHelper.TryLog("Program.ServiceRun[Error]",$"Wind 服务主机异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                    });
                    //启动
                    serviceConfigurator.OnStart((service,extraArguments)=>{
                        Helpers.LoggerModuleHelper.TryLog("Program.ServiceRun[Warning]",$"正在初始化 Wind 服务主机\n参数: {JsonSerializer.Serialize(extraArguments)}");
                        //运行权限
                        WindowsIdentity identity=WindowsIdentity.GetCurrent();
                        Helpers.LoggerModuleHelper.TryLog("Program.ServiceRun[Warning]",$"运行权限: {identity.Name}");
                        //启动服务
                        Helpers.LoggerModuleHelper.TryLog("Program.ServiceRun[Warning]",$"正在启动 Wind 服务主机");
                        service.Start();
                        Helpers.LoggerModuleHelper.TryLog("Program.ServiceRun[Warning]",$"已启动 Wind 服务主机");
                    });
                    //停止
                    serviceConfigurator.OnStop(service=>{
                        //启动服务
                        Helpers.LoggerModuleHelper.TryLog("Program.ServiceRun[Warning]",$"正在停止 Wind 服务主机");
                        service.Stop();
                        Helpers.LoggerModuleHelper.TryLog("Program.ServiceRun[Warning]",$"已停止 Wind 服务主机");
                    });
                });
            });
        }
    }
}
