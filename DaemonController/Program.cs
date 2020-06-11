using System;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DaemonController {
    public class Program {
        public static Entities.Common.AppEnvironment AppEnvironment{get;}=new Entities.Common.AppEnvironment();

        public static void Main(String[] args){
            if(args.GetLength(0)<1){
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
                //获取全部单元状态,0x11
                case "status-all":break;
                //启动全部单元,0x12
                case "start-all":break;
                //停止全部单元,0x13
                case "stop-all":break;
                //重启全部单元,0x14
                case "restart-all":break;
                //停止服务主机,0xFF
                case "shut-down":ShutDownCommand();break;
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
            Console.WriteLine("\"unitKey\" is the unit's file name,for \"example.json\",it's \"example\"");
            //Console.WriteLine("wind status <unitKey>     => get unit's status");
            //Console.WriteLine("wind start <unitKey>      => start unit");
            //Console.WriteLine("wind stop <unitKey>       => stop unit");
            //Console.WriteLine("wind restart <unitKey>    => restart unit");
            Console.WriteLine("wind shut-down            => shutdown daemon service");
            Console.WriteLine();
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
        /// 0xFF
        /// </summary>
        private static void ShutDownCommand() {
            Byte[] header=new Byte[8]{0xFF,0x00,0x00,0x00,0x00,0x00,0x00,0x00};
            Invoke(header);
        }
        
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
                ConsoleColor consoleColor=Console.ForegroundColor;
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine($"execute command exception,{exception.Message}");
                Console.ForegroundColor=consoleColor;
            }
        }

        /// <summary>
        /// 收到数据
        /// </summary>
        /// <param name="bytes"></param>
        private static void OnMessage(Byte[] bytes) {
            if(bytes[0]==0x00) {
                ConsoleColor consoleColor=Console.ForegroundColor;
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("execute command failed");
                Console.ForegroundColor=consoleColor;
                return;
            }
            if(bytes[0]==0xFF) {
                ConsoleColor consoleColor=Console.ForegroundColor;
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("daemon service shutting");
                Console.ForegroundColor=consoleColor;
                return;
            }
            String responseText=Encoding.UTF8.GetString(bytes,8,bytes.GetLength(0)-8).Trim();
            Console.WriteLine(responseText);
        }
    }
}
