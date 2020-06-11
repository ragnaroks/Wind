using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Daemon {
    public class Program {
        /// <summary>应用程序环境配置</summary>
        public static Entities.Common.AppEnvironment AppEnvironment{get;}=new Entities.Common.AppEnvironment();
        /// <summary>应用程序配置</summary>
        public static Entities.Common.AppSettings AppSettings{get;set;}=new Entities.Common.AppSettings();
        /// <summary>日志模块</summary>
        public static Modules.LoggerModule LoggerModule{get;}=new Modules.LoggerModule();
        /// <summary>服务</summary>
        public static IHost ServiceHost{get;private set;}=null;

        public static void Main(String[] args) {
            ServiceHost=CreateHostBuilder(args).UseWindowsService().Build();
            ServiceHost.Run();
        }

        public static IHostBuilder CreateHostBuilder(String[] args){
            return Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services)=>{
                services.AddHostedService<Worker>();
            });
        }
    }
}
