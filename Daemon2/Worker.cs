using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Daemon {
    public class Worker:BackgroundService {
        private IConfiguration Configuration{get;}=null;
        private ILogger<Worker> Logger{get;}=null;
        
        public Worker(ILogger<Worker> logger,IConfiguration configuration) {
            this.Configuration=configuration;
            this.Logger=logger;

            if(!this.Initialize()){
                Program.ServiceHost.StopAsync(new CancellationToken());
                return;
            }
        }

        public override async Task StartAsync(CancellationToken cancellationToken) {
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken) {
            await base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            while(!stoppingToken.IsCancellationRequested){
                Console.WriteLine("ExecuteAsync loop");
                await Task.Delay(1000,stoppingToken);
            }
        }

        private Boolean Initialize(){
            if(!Program.AppSettings.Setup(this.Configuration)){return false;}
            return true;
        }
    }
}
