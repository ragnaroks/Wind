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
                PrintVersion();
                return;
            }
            switch(args[0]) {
                case "start":StartUnit(args);break;
                case "help":PrintHelp();break;
                default:PrintVersion();break;
            }
            return;
            //整理一下操作类型
            // [0] => 0x00:parseAllUnits,0x01:parseUnit,0x02:startUnit,0x03:stopUnit,0x04:statusUnit
            // [1] => 保留
            // [2] => 保留
            // [3] => 保留
            // [4]+ unitKey
            Byte[] header=new Byte[]{0x00,0x00,0x00,0x00};
            Byte[] body=Encoding.UTF8.GetBytes("unitKey");
            Byte[] bytes=header.Concat(body).ToArray();
            Invoke(bytes);
        }

        private static void PrintVersion(){
            Version version=Assembly.GetExecutingAssembly().GetName().Version;
            Console.WriteLine($"Wind2 Daemon Service CommandLine Controller v{version}");
        }

        private static void PrintHelp() {
            Console.WriteLine("\"unitKey\" is the unit's file name,for \"example.json\",it's \"example\"");
            Console.WriteLine("wind start <unitKey>      => start unit");
            Console.WriteLine("wind stop <unitKey>       => stop unit");
            Console.WriteLine("wind restart <unitKey>    => restart unit");
            Console.WriteLine();
        }

        private static void StartUnit(String[] args){
            if(args.GetLength(0)<2){
                PrintHelp();
                return;
            }
            Byte[] header=new Byte[]{0x00,0x00,0x00,0x00};
            Byte[] body=Encoding.UTF8.GetBytes("unitKey");
            Byte[] bytes=header.Concat(body).ToArray();
            Invoke(bytes);
        }
        
        private static void Invoke(Byte[] bytes) {
            if(bytes.GetLength(0)>100){return;}
            try {
                NamedPipeClientStream namedPipeClientStream=new NamedPipeClientStream(".",AppEnvironment.PipelineName,PipeDirection.Out,PipeOptions.Asynchronous|PipeOptions.WriteThrough);
                namedPipeClientStream.Connect(1000);
                namedPipeClientStream.Write(bytes);
                namedPipeClientStream.Flush();
                namedPipeClientStream.Dispose();
            }catch(Exception exception) {
                Console.WriteLine("管道异常 "+exception.Message);
            }
        }
    }
}
