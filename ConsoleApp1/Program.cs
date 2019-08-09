using AsyncNet.Udp.Server;
using System;
using System.Threading;

namespace ConsoleApp1 {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            var server = new AsyncNetUdpServer(27015);
            server.ServerStarted += (s, e) => Console.WriteLine($"Server started on port: {e.ServerPort}");
            server.UdpPacketArrived += (s, e) =>
            {
                Console.WriteLine($"Server received: " +
                    $"{System.Text.Encoding.UTF8.GetString(e.PacketData)} " +
                    "from " +
                    $"[{e.RemoteEndPoint}]");

                var response = "Response!";
                var bytes = System.Text.Encoding.UTF8.GetBytes(response);
                server.Post(bytes, e.RemoteEndPoint);
            };
            server.StartAsync(CancellationToken.None);
            Console.ReadKey();
        }
    }
}
