using System;
using System.Diagnostics;
using PeterKottas.DotNetCore.WindowsService;

namespace Daemon {
    public static class Program {
        /// <summary>应用程序环境</summary>
        public readonly static Entities.AppEnvironment AppEnvironment=new Entities.AppEnvironment();
        /// <summary>应用程序配置</summary>
        public static Entities.AppSettings AppSettings;
        /// <summary>日志模块</summary>
        public static Modules.LoggerModule LoggerModule=new Modules.LoggerModule(Program.AppEnvironment.LogsDirectory,1000);
        /// <summary>应用程序配置模块</summary>
        public static Modules.AppSettingsModule AppSettingsModule=new Modules.AppSettingsModule(ref Program.AppSettings);
        /// <summary>性能计数器模块</summary>
        public static Modules.AppPerformanceCounterModule AppPerformanceCounterModule=new Modules.AppPerformanceCounterModule();
        /// <summary>WebSocket远程控制模块</summary>
        //public static Modules.WebSocketServerModule WebSocketServerModule;
        public static Modules.ControlServerModule ControlServerModule;
        /// <summary>单元控制模块</summary>
        public static Modules.UnitControlModule UnitControlModule;
        
        [STAThread]
        public static void Main(String[] args) {
            //如何得知服务主机被意外关闭?
            Process.GetCurrentProcess().EnableRaisingEvents=true;
            Process.GetCurrentProcess().Exited+=OnProgramExited;

            ServiceRunner<DaemonService>.Run(config=>{
                config.SetDisplayName("Wind2");
                config.SetName("Wind2");
                config.SetDescription("Wind2 Daemon Service");
                config.Service(serviceConfig=>{
                    //run
                    serviceConfig.ServiceFactory((extraArguments,microServiceController)=>new DaemonService(microServiceController));
                    //安装
                    serviceConfig.OnInstall(server=>{
                        Console.WriteLine("正在安装 Wind2");
                        Console.WriteLine("已安装 Wind2");
                    });
                    //卸载
                    serviceConfig.OnUnInstall(server=>{
                        Console.WriteLine("正在卸载 Wind2");
                        Console.WriteLine("已卸载 Wind2");
                    });
                    //继续
                    serviceConfig.OnContinue(server=>{
                        //
                    });
                    //暂停
                    serviceConfig.OnPause(server=>{
                        //
                    });
                    //Shutdown
                    serviceConfig.OnShutdown(server=>{
                        Console.WriteLine("Shutdown Wind2");
                    });
                    //错误
                    serviceConfig.OnError(exception=>{
                        ConsoleColor cc=Console.ForegroundColor;
                        Console.ForegroundColor=ConsoleColor.Green;
                        Console.WriteLine($"Program.Main => ServiceOnError | {exception.Message} | {exception.StackTrace}");
                        Console.ForegroundColor=cc;
                        Program.LoggerModule.Log("Program.Main[Error]",$"serviceConfigOnError,{exception.Message},{exception.StackTrace}");
                    });
                    //启动
                    serviceConfig.OnStart((service,extraArguments)=>{
                        Console.WriteLine("正在启动 Wind2");
                        /*
                        var identity = WindowsIdentity.GetCurrent();
                        var principal = new WindowsPrincipal(identity);
                        Program.Logger.Log("HostService","RunAs "+principal.Identity.Name);
                        */
                        service.Start();
                        Console.WriteLine("已启动 Wind2");
                    });
                    //停止
                    serviceConfig.OnStop(server=>{
                        Console.WriteLine("正在停止 Wind2");
                        server.Stop();
                        Console.WriteLine("已停止 Wind2");
                    });
                });
            });
        }

        private static void OnProgramExited(object sender,EventArgs e) {
            //停止所有运行单元
            if(Program.UnitControlModule!=null) {
                Program.UnitControlModule.Dispose();
            }
        }
    }
}
