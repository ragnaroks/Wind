using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WatsonWebsocket;
using wind.Entities.Protobuf;
using windctl.Helpers;

namespace windctl.Modules {
    public class WebSocketControlModule:IDisposable{
        public Boolean Useable{get;private set;}=false;

        /// <summary>ipv4正则</summary>
        private Regex RegexAddress4{get;}=new Regex(@"^[0-9\.]{7,15}$",RegexOptions.Compiled);
        /// <summary>ipv6正则</summary>
        private Regex RegexAddress6{get;}=new Regex(@"^[a-f0-9\:\[\]]{5,41}$",RegexOptions.Compiled);
        /// <summary>远程控制Key正则</summary>
        private Regex RegexControlKey{get;}=new Regex(@"^\S{32,4096}$",RegexOptions.Compiled);
        /// <summary>远程控制Key</summary>
        private String ControlKey{get;set;}=null;
        /// <summary>websocket客户端</summary>
        private WatsonWsClient Client{get;set;}=null;
        /// <summary>定时器</summary>
        private Timer Timer{get;set;}=null;
        /// <summary>是否启用定时器</summary>
        private Boolean TimerEnable{get;set;}=false;
        /// <summary>客户端链接GUID</summary>
        private String ClientConnectionId{get;set;}=null;
        /// <summary>客户端链接是否有效</summary>
        private Boolean ClientConnectionValid{get;set;}=false;
        
        #region IDisposable
        private bool disposedValue;
        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: 释放托管状态(托管对象)
                    if(this.Client!=null) {
                        //this.Client.ServerConnected-=this.ServerConnected;
                        //this.Client.ServerDisconnected-=this.ServerDisconnected;
                        this.Client.MessageReceived-=this.ClientMessageReceived;
                        this.Client.Dispose();
                    }
                    this.Timer?.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                disposedValue=true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~WebSocketControlModule()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose() {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        /// <param name="controlKey"></param>
        /// <returns></returns>
        public Boolean Setup(String address,Int32 port,String controlKey){
            if(this.Useable){return true;}
            //校验参数
            if(String.IsNullOrWhiteSpace(address) || port>Int16.MaxValue || port<1024){
                LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.Setup[Error]",$"初始化模块失败,参数错误\naddress:{address},port:{port}");
                return false;
            }
            if(address=="localhost"){ address="[::1]"; }
            Boolean isV4=this.RegexAddress4.IsMatch(address);
            Boolean isV6=this.RegexAddress6.IsMatch(address);
            if(!isV4 && !isV6){
                LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.Setup[Error]",$"初始化模块失败,参数错误\naddress:{address},port:{port}");
                return false;
            }
            if(String.IsNullOrWhiteSpace(controlKey) || !this.RegexControlKey.IsMatch(controlKey)) {
                LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.Setup[Error]",$"初始化模块失败,参数错误\ncontrolKey:{controlKey}");
                return false;
            }
            this.ControlKey=controlKey;
            //初始化定时器
            try {
                this.Timer=new Timer(this.TimerCallback,null,0,8000);
            }catch(Exception exception) {
                LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.Setup[Error]",$"初始化定时器异常\n异常信息:{exception.Message}\n异常堆栈:{exception.StackTrace}");
                return false;
            }
            this.TimerEnable=true;
            //初始化客户端
            try {
                this.Client=new WatsonWsClient(address,port,false);
            }catch(Exception exception) {
                LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.Setup[Error]",$"初始化客户端异常\n异常信息:{exception.Message}\n异常堆栈:{exception.StackTrace}");
                return false;
            }
            //this.Client.ServerConnected+=this.ServerConnected;
            //this.Client.ServerDisconnected+=this.ServerDisconnected;
            this.Client.MessageReceived+=this.ClientMessageReceived;
            //完成
            this.Useable=true;
            return true;
        }

        /// <summary>
        /// 链接服务段
        /// </summary>
        /// <returns></returns>
        public Boolean Start() {
            try {
                this.Client.Start();
            }catch(Exception exception) {
                LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.Setup[Error]",$"链接服务器异常\n异常信息:{exception.Message}\n异常堆栈:{exception.StackTrace}");
                return false;
            }
            SpinWait.SpinUntil(()=>this.Client.Connected,4000);
            return this.Client.Connected;//true or false(timeout)
        }

        /// <summary>
        /// 等待验证
        /// </summary>
        /// <returns></returns>
        public Boolean Valid() {
            SpinWait.SpinUntil(()=>this.ClientConnectionValid,4000);
            return this.ClientConnectionValid;
        }

        /// <summary>
        /// 定时器任务
        /// </summary>
        /// <param name="state"></param>
        private void TimerCallback(Object state) {
            if(!this.Useable || !this.TimerEnable){return;}
            this.TimerEnable=false;
            this.ClientKeepAlive();
            this.TimerEnable=true;
        }

        /// <summary>
        /// 收到服务端数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageReceivedEventArgs"></param>
        private void ClientMessageReceived(object sender,MessageReceivedEventArgs messageReceivedEventArgs){
            //解析数据
            if(messageReceivedEventArgs.Data.GetLength(0)<1){return;}
            PacketTestProtobuf packetTestProtobuf;
            try {
                packetTestProtobuf=PacketTestProtobuf.Parser.ParseFrom(messageReceivedEventArgs.Data);
            }catch(Exception exception){
                LoggerModuleHelper.TryLog(
                    "Modules.WebSocketControlModule.ClientMessageReceived[Error]",
                    $"探测数据包时异常\n异常信息:{exception.Message}\n异常堆栈:{exception.StackTrace}");
                return;
            }
            //分拣处理
            LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.ClientMessageReceived",$"分拣处理 {packetTestProtobuf.Type}");
            switch(packetTestProtobuf.Type) {
                case 21:this.ServerAcceptConnection(messageReceivedEventArgs.Data);break;
                case 22:this.ServerValidateConnection(messageReceivedEventArgs.Data);break;
                case 2001:this.StatusResponse(messageReceivedEventArgs.Data);break;
                case 2002:this.StartResponse(messageReceivedEventArgs.Data);break;
                case 2003:this.StopResponse(messageReceivedEventArgs.Data);break;
                case 2004:this.RestartResponse(messageReceivedEventArgs.Data);break;
                case 2005:this.LoadResponse(messageReceivedEventArgs.Data);break;
                case 2006:this.RemoveResponse(messageReceivedEventArgs.Data);break;
                default:break;
            }
        }

        #region 流程
        /// <summary>
        /// 定时发送心跳包
        /// </summary>
        private void ClientKeepAlive() {
            if(!this.Client.Connected){return;}
            ClientKeepAliveProtobuf clientKeepAliveProtobuf=new ClientKeepAliveProtobuf{Type=1};
            this.Client.SendAsync(clientKeepAliveProtobuf.ToByteArray()).Wait();
        }
        /// <summary>
        /// 客户端向服务端请求验证ControlKey
        /// </summary>
        private void ClientOfferControlKey(){
            if(!this.Client.Connected){return;}
            LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.ClientOfferControlKey","向服务端请求验证");
            ClientOfferControlKeyProtobuf clientOfferControlKeyProtobuf=new ClientOfferControlKeyProtobuf{
                Type=12,ConnectionId=this.ClientConnectionId,ControlKey=this.ControlKey};
            _=this.Client.SendAsync(clientOfferControlKeyProtobuf.ToByteArray());
        }
        /// <summary>
        /// 服务端响应客户端链接事件,并回复给客户端
        /// </summary>
        /// <param name="bytes"></param>
        private void ServerAcceptConnection(Byte[] bytes) {
            //解析数据
            ServerAcceptConnectionProtobuf serverAcceptConnectionProtobuf;
            try {
                serverAcceptConnectionProtobuf=ServerAcceptConnectionProtobuf.Parser.ParseFrom(bytes);
            }catch(Exception exception){
                LoggerModuleHelper.TryLog(
                    "Modules.WebSocketControlModule.ServerAcceptConnection[Error]",
                    $"解析数据包时异常\n异常信息:{exception.Message}\n异常堆栈:{exception.StackTrace}");
                return;
            }
            //设置客户端链接Id
            LoggerModuleHelper.TryLog(
                "Modules.WebSocketControlModule.ServerAcceptConnection",$"已收到客户端链接Id {serverAcceptConnectionProtobuf.ConnectionId}");
            this.ClientConnectionId=serverAcceptConnectionProtobuf.ConnectionId;
            //请求验证客户端
            this.ClientOfferControlKey();
        }
        /// <summary>
        /// 服务端回复客户端ControlKey验证结果
        /// </summary>
        /// <param name="bytes"></param>
        private void ServerValidateConnection(Byte[] bytes){
            //解析数据
            ServerValidateConnectionProtobuf serverValidateConnectionProtobuf;
            try {
                serverValidateConnectionProtobuf=ServerValidateConnectionProtobuf.Parser.ParseFrom(bytes);
            }catch(Exception exception){
                LoggerModuleHelper.TryLog(
                    "Modules.WebSocketControlModule.ServerValidateConnection[Error]",
                    $"解析数据包时异常\n异常信息:{exception.Message}\n异常堆栈:{exception.StackTrace}");
                return;
            }
            //验证结果
            LoggerModuleHelper.TryLog(
                "Modules.WebSocketControlModule.ServerValidateConnection",$"已收到客户端验证结果 {serverValidateConnectionProtobuf.Valid}");
            this.ClientConnectionValid=serverValidateConnectionProtobuf.Valid;//用于解除自旋锁
        }
        #endregion

        #region 客户端请求
        /// <summary>
        /// windctl status unitKey
        /// 1001
        /// </summary>
        /// <param name="unitKey"></param>
        public void StatusRequest(String unitKey) {
            if(!this.Client.Connected || !this.ClientConnectionValid || Program.InAction){return;}
            StatusRequestProtobuf statusRequestProtobuf=new StatusRequestProtobuf{Type=1001,UnitKey=unitKey};
            _=this.Client.SendAsync(statusRequestProtobuf.ToByteArray());
            Program.InAction=true;
        }
        /// <summary>
        /// windctl start unitKey
        /// 1002
        /// </summary>
        /// <param name="unitKey"></param>
        public void StartRequest(String unitKey) {
            if(!this.Client.Connected || !this.ClientConnectionValid || Program.InAction){return;}
            StartRequestProtobuf startRequestProtobuf=new StartRequestProtobuf{Type=1002,UnitKey=unitKey};
            _=this.Client.SendAsync(startRequestProtobuf.ToByteArray());
            Program.InAction=true;
        }
        /// <summary>
        /// windctl stop unitKey
        /// 1003
        /// </summary>
        /// <param name="unitKey"></param>
        public void StopRequest(String unitKey) {
            if(!this.Client.Connected || !this.ClientConnectionValid || Program.InAction){return;}
            StopRequestProtobuf stopRequestProtobuf=new StopRequestProtobuf{Type=1003,UnitKey=unitKey};
            _=this.Client.SendAsync(stopRequestProtobuf.ToByteArray());
            Program.InAction=true;
        }
        /// <summary>
        /// windctl restart unitKey
        /// 1004
        /// </summary>
        /// <param name="unitKey"></param>
        public void RestartRequest(String unitKey) {
            if(!this.Client.Connected || !this.ClientConnectionValid || Program.InAction){return;}
            RestartRequestProtobuf restartRequestProtobuf=new RestartRequestProtobuf{Type=1004,UnitKey=unitKey};
            _=this.Client.SendAsync(restartRequestProtobuf.ToByteArray());
            Program.InAction=true;
        }
        /// <summary>
        /// windctl load unitKey
        /// 1005
        /// </summary>
        /// <param name="unitKey"></param>
        public void LoadRequest(String unitKey) {
            if(!this.Client.Connected || !this.ClientConnectionValid || Program.InAction){return;}
            LoadRequestProtobuf loadRequestProtobuf=new LoadRequestProtobuf{Type=1005,UnitKey=unitKey};
            _=this.Client.SendAsync(loadRequestProtobuf.ToByteArray());
            Program.InAction=true;
        }
        /// <summary>
        /// windctl remove unitKey
        /// 1006
        /// </summary>
        /// <param name="unitKey"></param>
        public void RemoveRequest(String unitKey) {
            if(!this.Client.Connected || !this.ClientConnectionValid || Program.InAction){return;}
            RemoveRequestProtobuf removeRequestProtobuf=new RemoveRequestProtobuf{Type=1006,UnitKey=unitKey};
            _=this.Client.SendAsync(removeRequestProtobuf.ToByteArray());
            Program.InAction=true;
        }
        #endregion

        #region 服务端响应
        /// <summary>
        /// windctl status unitKey
        /// 2001
        /// </summary>
        /// <param name="bytes"></param>
        private void StatusResponse(Byte[] bytes) {
            //解析数据
            StatusResponseProtobuf statusResponseProtobuf;
            try {
                statusResponseProtobuf=StatusResponseProtobuf.Parser.ParseFrom(bytes);
            }catch(Exception exception){
                LoggerModuleHelper.TryLog(
                    "Modules.WebSocketControlModule.StatusResponse[Error]",
                    $"解析数据包时异常\n异常信息:{exception.Message}\n异常堆栈:{exception.StackTrace}");
                return;
            }
            //调用
            CommandHelper.Status(statusResponseProtobuf);
            Program.InAction=false;
        }
        /// <summary>
        /// windctl start unitKey
        /// 2002
        /// </summary>
        /// <param name="bytes"></param>
        private void StartResponse(Byte[] bytes) {
            //解析数据
            StartResponseProtobuf startResponseProtobuf;
            try {
                startResponseProtobuf=StartResponseProtobuf.Parser.ParseFrom(bytes);
            }catch(Exception exception){
                LoggerModuleHelper.TryLog(
                    "Modules.WebSocketControlModule.StartResponse[Error]",
                    $"解析数据包时异常\n异常信息:{exception.Message}\n异常堆栈:{exception.StackTrace}");
                return;
            }
            //调用
            CommandHelper.Start(startResponseProtobuf);
            Program.InAction=false;
        }
        /// <summary>
        /// windctl start unitKey
        /// 2003
        /// </summary>
        /// <param name="bytes"></param>
        private void StopResponse(Byte[] bytes) {
            //解析数据
            StopResponseProtobuf stopResponseProtobuf;
            try {
                stopResponseProtobuf=StopResponseProtobuf.Parser.ParseFrom(bytes);
            }catch(Exception exception){
                LoggerModuleHelper.TryLog(
                    "Modules.WebSocketControlModule.StopResponse[Error]",
                    $"解析数据包时异常\n异常信息:{exception.Message}\n异常堆栈:{exception.StackTrace}");
                return;
            }
            //调用
            CommandHelper.Stop(stopResponseProtobuf);
            Program.InAction=false;
        }
        /// <summary>
        /// windctl restart unitKey
        /// 2004
        /// </summary>
        /// <param name="bytes"></param>
        private void RestartResponse(Byte[] bytes) {
            //解析数据
            RestartResponseProtobuf restartResponseProtobuf;
            try {
                restartResponseProtobuf=RestartResponseProtobuf.Parser.ParseFrom(bytes);
            }catch(Exception exception){
                LoggerModuleHelper.TryLog(
                    "Modules.WebSocketControlModule.RestartResponse[Error]",
                    $"解析数据包时异常\n异常信息:{exception.Message}\n异常堆栈:{exception.StackTrace}");
                return;
            }
            //调用
            CommandHelper.Restart(restartResponseProtobuf);
            Program.InAction=false;
        }
        /// <summary>
        /// windctl load unitKey
        /// 2005
        /// </summary>
        /// <param name="bytes"></param>
        private void LoadResponse(Byte[] bytes) {
            //解析数据
            LoadResponseProtobuf loadResponseProtobuf;
            try {
                loadResponseProtobuf=LoadResponseProtobuf.Parser.ParseFrom(bytes);
            }catch(Exception exception){
                LoggerModuleHelper.TryLog(
                    "Modules.WebSocketControlModule.LoadResponse[Error]",
                    $"解析数据包时异常\n异常信息:{exception.Message}\n异常堆栈:{exception.StackTrace}");
                return;
            }
            //调用
            CommandHelper.Load(loadResponseProtobuf);
            Program.InAction=false;
        }
        /// <summary>
        /// windctl remove unitKey
        /// 2006
        /// </summary>
        /// <param name="bytes"></param>
        private void RemoveResponse(Byte[] bytes) {
            //解析数据
            RemoveResponseProtobuf removeResponseProtobuf;
            try {
                removeResponseProtobuf=RemoveResponseProtobuf.Parser.ParseFrom(bytes);
            }catch(Exception exception){
                LoggerModuleHelper.TryLog(
                    "Modules.WebSocketControlModule.RemoveResponse[Error]",
                    $"解析数据包时异常\n异常信息:{exception.Message}\n异常堆栈:{exception.StackTrace}");
                return;
            }
            //调用
            CommandHelper.Remove(removeResponseProtobuf);
            Program.InAction=false;
        }
        #endregion
    }
}
