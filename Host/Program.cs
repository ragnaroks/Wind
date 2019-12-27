using Newtonsoft.Json;
using PeterKottas.DotNetCore.WindowsService;
using System;
using System.Collections.Generic;
using System.IO;

namespace Host {
    public class Program {
        /// <summary>
        /// 环境
        /// </summary>
        public static Entity.Settings Settings=new Entity.Settings();
        /// <summary>
        /// 配置
        /// </summary>
        public static Entity.AppSettings AppSettings;
        /// <summary>
        /// 日志模块
        /// </summary>
        public static Module.Logger Logger=new Module.Logger(Program.Settings.LogDirectory,1000);
        /// <summary>
        /// Udp被控
        /// </summary>
        public static Module.UdpSocketServer UdpSocketServer=null;
        /// <summary>
        /// 单元
        /// </summary>
        public static Dictionary<String,Entity.Unit> Units=new Dictionary<String, Entity.Unit>();
                
        public static void Main(String[] args){
            //读取配置
            if(!File.Exists(Program.Settings.CurrentDirectory+Path.DirectorySeparatorChar+"AppSettings.json")){
                Console.WriteLine("配置文件不存在");
                Program.Logger.Log("HostService","配置文件不存在");
            return;}
            FileStream fs1=null;
            try {
                fs1=File.Open(Program.Settings.CurrentDirectory+Path.DirectorySeparatorChar+"AppSettings.json",FileMode.Open,FileAccess.Read,FileShare.Read);
                if(fs1.Length>Int32.MaxValue){return;}
                Byte[] buffer=new Byte[fs1.Length];
                fs1.Read(buffer,0,(Int32)fs1.Length);
                Program.AppSettings=JsonConvert.DeserializeObject<Entity.AppSettings>(System.Text.Encoding.UTF8.GetString(buffer));
            }catch(Exception exception){
                Console.WriteLine("读取配置异常,"+exception.Message);
                Program.Logger.Log("HostService","读取配置异常,"+exception.Message);
                return;
            } finally {
                fs1.Dispose();
            }
            if(Program.AppSettings==null){return;}
            //Udp被控模块
            if(Program.AppSettings.ControlEnable){Program.UdpSocketServer=new Module.UdpSocketServer();}

            ServiceRunner<HostService>.Run(config=>{
                config.SetDisplayName("Wind2");
                config.SetName("Wind2");
                config.SetDescription("Wind2 Services Host");
                config.Service(serviceConfig=>{
                    //run
                    serviceConfig.ServiceFactory((extraArguments,microServiceController)=>{
                        return new HostService(microServiceController);
                    });
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
                        Console.WriteLine("正在恢复 Wind2");
                        Program.Settings.IsPaused=false;
                        Console.WriteLine("已恢复 Wind2");
                    });
                    //暂停
                    serviceConfig.OnPause(server=>{
                        Console.WriteLine("正在暂停 Wind2");
                        Program.Settings.IsPaused=true;
                        Console.WriteLine("已暂停 Wind2");
                    });
                    //Shutdown
                    serviceConfig.OnShutdown(server=>{
                        Console.WriteLine("Shutdown Wind2");
                    });
                    //错误
                    serviceConfig.OnError(exception=>{
                        Console.WriteLine("Wind2 异常: "+exception.Message+" | "+exception.StackTrace);
                        Program.Logger.Log("HostService","异常: "+exception.Message+" | "+exception.StackTrace);
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
    }
}
