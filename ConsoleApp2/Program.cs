using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using ConsoleApp2.Helper;

namespace ConsoleApp2 {
    static class Program {
        /// <summary>
        /// 程序集根目录
        /// </summary>
        private static String RootDirectory{get;}=Environment.CurrentDirectory;
        /// <summary>
        /// 应用程序配置
        /// </summary>
        private static AppSettings AppSettings{get;set;}=null;
        /// <summary>
        /// 客户端套接字
        /// </summary>
        private static Socket Socket{get;set;}=null;

        static void Main(string[] _args) {
            Console.BackgroundColor=ConsoleColor.Black;
            Console.ForegroundColor=ConsoleColor.White;
            Console.WriteLine("Hello World!");

            Console.WriteLine("读取配置文件");
            if(!File.Exists(Program.RootDirectory+Path.DirectorySeparatorChar+"AppSettings.json")){
                Console.ForegroundColor=ConsoleColor.Yellow;
                Console.WriteLine("读取配置文件失败,配置文件不存在");
                Console.ForegroundColor=ConsoleColor.White;
                Console.ReadKey();
            return;}
            FileStream fs1=null;

            try{
                fs1=File.OpenRead(Program.RootDirectory+Path.DirectorySeparatorChar+"AppSettings.json");
                Byte[] bs1=new Byte[fs1.Length];
                fs1.Read(bs1,0,(Int32)fs1.Length);
                fs1.Dispose();
                Program.AppSettings=JsonConvert.DeserializeObject<AppSettings>(Encoding.UTF8.GetString(bs1));
            }catch(Exception _e){
                fs1.Dispose();
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("读取配置文件异常,"+_e.Message+","+_e.StackTrace);
                Console.ForegroundColor=ConsoleColor.White;
                Console.ReadKey();
            return;}
            Console.ForegroundColor=ConsoleColor.Green;
            Console.WriteLine("读取配置文件完成");
            Console.ForegroundColor=ConsoleColor.White;

            Console.WriteLine("初始化套接字");
            IPEndPoint ipep1=new IPEndPoint(IPAddress.Parse(Program.AppSettings.HostAddress),Program.AppSettings.HostPort);//这里懒得检查了
            EndPoint ipep2=new IPEndPoint(IPAddress.Any,0);
            Program.Socket=new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp){Blocking=true};//阻塞等待回复
            try {
                Program.Socket.SendTo(AesEncrypt.Encrypt(Program.AppSettings.HostKey,"{\"ActionId\":1,\"ActionName\":\"Hello\"}"),ipep1);
            }catch(Exception _e) {
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("套接字数据发送异常,"+_e.Message+","+_e.StackTrace);
                Console.ForegroundColor=ConsoleColor.White;
                Console.ReadKey();
            return;}
            Byte[] bs2=new Byte[1024];
            Byte[] bs2_2=new Byte[Program.Socket.ReceiveFrom(bs2,ref ipep2)];
            for(Int32 i = 0;i<bs2_2.Length;i++) {bs2_2[i]=bs2[i];}
            dynamic d1=JsonConvert.DeserializeObject(AesEncrypt.Decrypt(Program.AppSettings.HostKey,bs2_2));
            if(d1.ErrorCode==null || d1.ErrorCode>0){
                Console.ForegroundColor=ConsoleColor.Yellow;
                Console.WriteLine("初始化套接字失败");
                Console.ForegroundColor=ConsoleColor.White;
                Console.ReadKey();
            return;}
            Console.ForegroundColor=ConsoleColor.Green;
            Console.WriteLine("初始化套接字完成,在下面输入JSON");
            Console.ForegroundColor=ConsoleColor.White;

            while (true) {
                EndPoint ep=new IPEndPoint(IPAddress.Any,0);
                String input=Console.ReadLine();
                if(input=="exit"){
                    Program.Socket.Disconnect(true);
                    Program.Socket.Dispose();
                    Console.WriteLine("初始化套已释放");
                    Console.ReadKey();
                break;}
                try {
                    Program.Socket.SendTo(AesEncrypt.Encrypt(Program.AppSettings.HostKey,input),ipep1);
                }catch(Exception _e) {
                    Console.ForegroundColor=ConsoleColor.Red;
                    Console.WriteLine("套接字数据发送异常,"+_e.Message+","+_e.StackTrace);
                    Console.ForegroundColor=ConsoleColor.White;
                    Console.ReadKey();
                continue;}
                Byte[] buffer=new Byte[1024];
                Byte[] buffer_real=new Byte[Program.Socket.ReceiveFrom(buffer,ref ep)];
                for(Int32 i = 0;i<buffer_real.Length;i++) {buffer_real[i]=buffer[i];}
                Console.ForegroundColor=ConsoleColor.Cyan;
                Console.WriteLine("套接字收到消息,"+AesEncrypt.Decrypt(Program.AppSettings.HostKey,buffer_real));
                Console.ForegroundColor=ConsoleColor.White;
            }
        }
    }
}
