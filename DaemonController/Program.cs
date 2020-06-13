using DaemonController.Helper;
using System;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DaemonController {
    public static class Program {
        public static Entities.Common.AppEnvironment AppEnvironment{get;}=new Entities.Common.AppEnvironment();

        public static void Main(String[] args){
            if(args==null || args.GetLength(0)<1){
                PrintHelp();
                return;
            }
            switch(args[0]){
                //获取单元状态,0x01
                case "status":StatusCommand(args);break;
                //启动单元,0x02
                case "start":StartCommand(args);break;
                //停止单元,0x03
                case "stop":StopCommand(args);break;
                //重启单元,0x04
                case "restart":RestartCommand(args);break;
                //加载单元,0x05,服务主机运行时添加新单元配置文件,需要此指令来加载(不会自动运行)
                case "load":LoadCommand(args);break;
                //移除单元,0x06,从服务主机移除单元配置文件并停止单元运行(不会物理删除文件)
                case "remove":RemoveCommand(args);break;
                //获取全部单元状态,0x11
                //case "status-all":break;
                //启动全部单元,0x12
                case "start-all":StartAllCommand();break;
                //停止全部单元,0x13
                case "stop-all":StopAllCommand();break;
                //重启全部单元,0x14
                case "restart-all":RestartAllCommand();break;
                //加载单元,0x015
                case "load-all":LoadAllCommand();break;
                //移除单元,0x016
                case "remove-all":RemoveAllCommand();break;
                //获取主机版本,0xF0
                case "daemon-version":DaemonVersionCommand();break;
                //停止服务主机,0xFF
                case "daemon-shutdown":DaemonShutdownCommand();break;
                //版本
                case "version":PrintVersion();break;
                //默认
                default:PrintHelp();break;
            }
        }

        /// <summary>
        /// 版本
        /// </summary>
        private static void PrintVersion(){
            Version version=Assembly.GetExecutingAssembly().GetName().Version;
            Console.WriteLine($"Wind2 Daemon Service CommandLine Controller v{version}");
        }

        /// <summary>
        /// 帮助信息
        /// </summary>
        private static void PrintHelp() {
            Console.WriteLine("windctl version              => print this tool's version");
            ConsoleHelper.ColorWrite("windctl status §b<unitKey>§|     => get unit's status\n");
            ConsoleHelper.ColorWrite("windctl start §b<unitKey>§|      => start unit\n");
            ConsoleHelper.ColorWrite("windctl stop §b<unitKey>§|       => stop unit\n");
            ConsoleHelper.ColorWrite("windctl restart §b<unitKey>§|    => restart unit\n");
            ConsoleHelper.ColorWrite("windctl load §b<unitKey>§|       => try load/update unit's settings from file\n");
            ConsoleHelper.ColorWrite("windctl remove §b<unitKey>§|     => stop unit and remove it,it can not be start again\n");
            Console.WriteLine("windctl start-all            => start all unit");
            Console.WriteLine("windctl stop-all             => stop all unit");
            Console.WriteLine("windctl restart-all          => restart all unit");
            Console.WriteLine("windctl load-all             => try load/update all units's settings from file");
            Console.WriteLine("windctl remove-all           => stop all unit and remove them,they can not be start again");
            Console.WriteLine("windctl daemon-version       => get daemon service's version");
            ConsoleHelper.ColorWrite("windctl §cdaemon-shutdown§|      => shutdown daemon service\n");
        }

        /// <summary>
        /// 0x01
        /// </summary>
        /// <param name="args"></param>
        private static void StatusCommand(String[] args){
            if(args.GetLength(0)<2){
                PrintHelp();
                return;
            }
            Byte[] header=new Byte[8]{0x01,0x00,0x00,0x00,0x00,0x00,0x00,0x00};
            Byte[] body=Encoding.UTF8.GetBytes(args[1]);
            Byte[] bytes=header.Concat(body).ToArray();
            Invoke(bytes);
        }
        /// <summary>
        /// 0x02
        /// </summary>
        /// <param name="args"></param>
        private static void StartCommand(String[] args){
            if(args.GetLength(0)<2){
                PrintHelp();
                return;
            }
            Byte[] header=new Byte[8]{0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00};
            Byte[] body=Encoding.UTF8.GetBytes(args[1]);
            Byte[] bytes=header.Concat(body).ToArray();
            Invoke(bytes);
        }
        /// <summary>
        /// 0x03
        /// </summary>
        /// <param name="args"></param>
        private static void StopCommand(String[] args){
            if(args.GetLength(0)<2){
                PrintHelp();
                return;
            }
            Byte[] header=new Byte[8]{0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00};
            Byte[] body=Encoding.UTF8.GetBytes(args[1]);
            Byte[] bytes=header.Concat(body).ToArray();
            Invoke(bytes);
        }
        /// <summary>
        /// 0x04
        /// </summary>
        /// <param name="args"></param>
        private static void RestartCommand(String[] args){
            if(args.GetLength(0)<2){
                PrintHelp();
                return;
            }
            Byte[] header=new Byte[8]{0x04,0x00,0x00,0x00,0x00,0x00,0x00,0x00};
            Byte[] body=Encoding.UTF8.GetBytes(args[1]);
            Byte[] bytes=header.Concat(body).ToArray();
            Invoke(bytes);
        }
        /// <summary>
        /// 0x05
        /// </summary>
        /// <param name="args"></param>
        private static void LoadCommand(String[] args){
            if(args.GetLength(0)<2){
                PrintHelp();
                return;
            }
            Byte[] header=new Byte[8]{0x05,0x00,0x00,0x00,0x00,0x00,0x00,0x00};
            Byte[] body=Encoding.UTF8.GetBytes(args[1]);
            Byte[] bytes=header.Concat(body).ToArray();
            Invoke(bytes);
        }
        /// <summary>
        /// 0x06
        /// </summary>
        /// <param name="args"></param>
        private static void RemoveCommand(String[] args){
            if(args.GetLength(0)<2){
                PrintHelp();
                return;
            }
            Byte[] header=new Byte[8]{0x06,0x00,0x00,0x00,0x00,0x00,0x00,0x00};
            Byte[] body=Encoding.UTF8.GetBytes(args[1]);
            Byte[] bytes=header.Concat(body).ToArray();
            Invoke(bytes);
        }

        /// <summary>
        /// 0x12
        /// </summary>
        private static void StartAllCommand()=>Invoke(new Byte[8]{0x12,0x00,0x00,0x00,0x00,0x00,0x00,0x00});
        /// <summary>
        /// 0x13
        /// </summary>
        private static void StopAllCommand()=>Invoke(new Byte[8]{0x13,0x00,0x00,0x00,0x00,0x00,0x00,0x00});
        /// <summary>
        /// 0x14
        /// </summary>
        private static void RestartAllCommand()=>Invoke(new Byte[8]{0x14,0x00,0x00,0x00,0x00,0x00,0x00,0x00});
        /// <summary>
        /// 0x15
        /// </summary>
        private static void LoadAllCommand()=>Invoke(new Byte[8]{0x15,0x00,0x00,0x00,0x00,0x00,0x00,0x00});
        /// <summary>
        /// 0x16
        /// </summary>
        private static void RemoveAllCommand()=>Invoke(new Byte[8]{0x16,0x00,0x00,0x00,0x00,0x00,0x00,0x00});

        /// <summary>
        /// 0xF0
        /// </summary>
        private static void DaemonVersionCommand()=>Invoke(new Byte[8]{0xF0,0x00,0x00,0x00,0x00,0x00,0x00,0x00});
        /// <summary>
        /// 0xFF
        /// </summary>
        private static void DaemonShutdownCommand()=>Invoke(new Byte[8]{0xFF,0x00,0x00,0x00,0x00,0x00,0x00,0x00});
        
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="bytes"></param>
        private static void Invoke(Byte[] bytes) {
            if(bytes.GetLength(0)>104){return;}
            try {
                NamedPipeClientStream namedPipeClientStream=new NamedPipeClientStream(".",AppEnvironment.PipelineName,PipeDirection.InOut,PipeOptions.Asynchronous|PipeOptions.WriteThrough);
                namedPipeClientStream.Connect(1000);
                //发送
                namedPipeClientStream.Write(bytes);
                namedPipeClientStream.Flush();
                //回复
                Byte[] responseBytes=new Byte[8192];
                namedPipeClientStream.Read(responseBytes,0,8192);
                OnMessage(responseBytes);
                //释放
                namedPipeClientStream.Dispose();
            }catch(Exception exception){
                ConsoleHelper.ColorWrite($"§cexecute command exception,{exception.Message}§|\n");
            }
        }

        /// <summary>
        /// 收到数据
        /// </summary>
        /// <param name="bytes"></param>
        private static void OnMessage(Byte[] bytes) {
            String responseText;
            try {
                responseText=Encoding.UTF8.GetString(bytes,8,bytes.GetLength(0)-8).TrimEnd().TrimEnd(new Char[]{'\0'});
            }catch(Exception exception){
                responseText="{"+exception.Message+"}";
            }
            if(bytes[0]==0x00) {
                ConsoleHelper.ColorWrite($"§cexecute command failed{(String.IsNullOrWhiteSpace(responseText)?String.Empty:","+responseText)}§|\n");
                return;
            }
            if(bytes[0]==0xFF) {
                ConsoleHelper.ColorWrite($"§cdaemon service shutting§|\n");
                return;
            }
            ConsoleHelper.ColorWrite(responseText);
            Console.WriteLine();
        }
    }
}
