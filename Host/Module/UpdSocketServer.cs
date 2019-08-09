using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AsyncNet.Udp.Server;
using Host.Helper;

namespace Host.Module {
    public class UdpSocketServer{
        private AsyncNetUdpServer AsyncNetUdpServer{get;set;}

        public UdpSocketServer() {
            if(!Program.AppSettings.ControlEnable){return;}
            if(String.IsNullOrWhiteSpace(Program.AppSettings.ControlAddress) || String.IsNullOrWhiteSpace(Program.AppSettings.ControlKey) || Program.AppSettings.ControlPort<1024){Program.Logger.Log("UdpSocketServer","参数缺失");return;}
            Regex regex_ip=new Regex(@"^[a-f\d\.\:]{3,39}$",RegexOptions.Compiled);
            if(!regex_ip.IsMatch(Program.AppSettings.ControlAddress) && Program.AppSettings.ControlAddress!="any" && Program.AppSettings.ControlAddress!="localhost"){Program.Logger.Log("UdpSocketServer","ControlAddress 配置有误");return;}
            IPAddress ip=null;
            switch (Program.AppSettings.ControlAddress) {
                case "any":ip=IPAddress.Any;break;//ip=IPAddress.IPv6Any;
                case "localhost":ip=IPAddress.Loopback;break;//ip=IPAddress.IPv6Loopback;
                default:if(!IPAddress.TryParse(Program.AppSettings.ControlAddress,out ip)){Program.Logger.Log("UdpSocketServer","ControlAddress 配置有误");return;}break;
            }
            try{IPEndPoint ipep=new IPEndPoint(ip,Program.AppSettings.ControlPort);}catch(Exception _e){Program.Logger.Log("UdpSocketServer","创建端点异常,"+_e.Message);return;}//只是为了验证是否能创建端点
            AsyncNetUdpServerConfig config=new AsyncNetUdpServerConfig{IPAddress=ip,Port=Program.AppSettings.ControlPort};
            this.AsyncNetUdpServer=new AsyncNetUdpServer(config);
            this.AsyncNetUdpServer.ServerStarted+=this.AsyncNetUdpServer_ServerStarted;
            this.AsyncNetUdpServer.ServerStopped+=this.AsyncNetUdpServer_ServerStopped;
            this.AsyncNetUdpServer.ServerExceptionOccured+=this.AsyncNetUdpServer_ServerExceptionOccured;
            this.AsyncNetUdpServer.UdpPacketArrived+=this.AsyncNetUdpServer_UdpPacketArrived;
            this.AsyncNetUdpServer.UdpSendErrorOccured+=this.AsyncNetUdpServer_UdpSendErrorOccured;
            this.AsyncNetUdpServer.StartAsync();
        }

        private void AsyncNetUdpServer_ServerStarted(object sender,AsyncNet.Udp.Server.Events.UdpServerStartedEventArgs e) {
            Console.WriteLine("UdpSocketServer Started");
            Program.Logger.Log("UdpSocketServer","Started");
        }
        private void AsyncNetUdpServer_ServerStopped(object sender,AsyncNet.Udp.Server.Events.UdpServerStoppedEventArgs e) {
            Console.WriteLine("UdpSocketServer Stopped");
            Program.Logger.Log("UdpSocketServer","Stopped");
        }
        private void AsyncNetUdpServer_ServerExceptionOccured(object sender,AsyncNet.Udp.Server.Events.UdpServerExceptionEventArgs e) {
            Console.WriteLine("UdpSocketServer ExceptionOccured,"+e.Exception.Message);
            Program.Logger.Log("UdpSocketServer","ExceptionOccured,"+e.Exception.Message);
        }
        private void AsyncNetUdpServer_UdpPacketArrived(object sender,AsyncNet.Udp.Remote.Events.UdpPacketArrivedEventArgs e) {
            Console.WriteLine("UdpSocketServer UdpPacketArrived,"+e.PacketData.GetString_Utf8());
            this.AsyncNetUdpServer.Post($"Hello {e.RemoteEndPoint.Address}:{e.RemoteEndPoint.Port}".GetBytes_Utf8(),e.RemoteEndPoint);
        }
        private void AsyncNetUdpServer_UdpSendErrorOccured(object sender,AsyncNet.Udp.Error.Events.UdpSendErrorEventArgs e) {
            Console.WriteLine("UdpSocketServer UdpSendErrorOccured,"+e.Exception.Message);
            Program.Logger.Log("UdpSocketServer","UdpSendErrorOccured,"+e.Exception.Message);
        }
    }
}
