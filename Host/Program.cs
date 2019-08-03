using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using PeterKottas.DotNetCore.WindowsService;

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
        public static Module.Logger Logger=new Module.Logger(Program.Settings.LogDirectory);
        /// <summary>
        /// 单元
        /// </summary>
        public static Dictionary<String,Entity.Unit> Units=new Dictionary<String, Entity.Unit>();
        
        public static void Main(String[] _args){
            ServiceRunner<HostService>.Run(_config=>{
                _config.SetDisplayName("Wind2");
                _config.SetName("Wind2");
                _config.SetDescription("Wind2 Services Host,version "+System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
                _config.Service(_serviceConfig=>{
                    //run
                    _serviceConfig.ServiceFactory((_extraArguments,_microServiceController)=>{
                        return new HostService(_microServiceController);
                    });
                    //安装
                    _serviceConfig.OnInstall(_server=>{
                        Console.WriteLine("正在安装 Wind2");
                        Console.WriteLine("已安装 Wind2");
                    });
                    //卸载
                    _serviceConfig.OnUnInstall(_server=>{
                        Console.WriteLine("正在卸载 Wind2");
                        Console.WriteLine("已卸载 Wind2");
                    });
                    //继续
                    _serviceConfig.OnContinue(_server=>{
                        Console.WriteLine("正在恢复 Wind2");
                        Program.Settings.IsPaused=false;
                        Console.WriteLine("已恢复 Wind2");
                    });
                    //暂停
                    _serviceConfig.OnPause(_server=>{
                        Console.WriteLine("正在暂停 Wind2");
                        Program.Settings.IsPaused=true;
                        Console.WriteLine("已暂停 Wind2");
                    });
                    //Shutdown
                    _serviceConfig.OnShutdown(_server=>{
                        Console.WriteLine("Shutdown Wind2");
                    });
                    //错误
                    _serviceConfig.OnError(_ex=>{
                        Console.WriteLine("Wind2 异常: "+_ex.Message+" | "+_ex.StackTrace);
                        Program.Logger.Log("HostService","异常: "+_ex.Message+" | "+_ex.StackTrace);
                    });
                    //启动
                    _serviceConfig.OnStart((_service,_extraArguments)=>{
                        if(!File.Exists(Program.Settings.CurrentDirectory+Path.DirectorySeparatorChar+"AppSettings.json")){
                            Console.WriteLine("配置文件不存在");
                            _service.Stop();
                        return;}
                        try {
                            FileStream fs1=File.Open(Program.Settings.CurrentDirectory+Path.DirectorySeparatorChar+"AppSettings.json",FileMode.Open,FileAccess.Read,FileShare.Read);
                            if(fs1.Length>Int32.MaxValue){_service.Stop();return;}
                            Byte[] buffer=new Byte[fs1.Length];
                            fs1.Read(buffer,0,(Int32)fs1.Length);
                            fs1.Dispose();
                            Program.AppSettings=JsonConvert.DeserializeObject<Entity.AppSettings>(System.Text.Encoding.UTF8.GetString(buffer));
                        }catch(Exception _e){
                            Console.WriteLine("读取配置异常,"+_e.Message);
                            _service.Stop();
                            return;
                        }
                        if(Program.AppSettings==null){_service.Stop();return;}
                        Console.WriteLine("正在启动 Wind2");
                        _service.Start();
                        Console.WriteLine("已启动 Wind2");
                    });
                    //停止
                    _serviceConfig.OnStop(_server=>{
                        Console.WriteLine("正在停止 Wind2");
                        _server.Stop();
                        Console.WriteLine("已停止 Wind2");
                    });
                });
            });
        }
    }
}
