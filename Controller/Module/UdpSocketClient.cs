using AsyncNet.Udp.Client;
using AsyncNet.Udp.Client.Events;
using AsyncNet.Udp.Error.Events;
using AsyncNet.Udp.Remote.Events;
using System;

namespace Controller.Module {
    public class UdpSocketClient:IDisposable{
        private AsyncNetUdpClient AsyncNetUdpClient{get;set;}
        private ViewModel.MainWindowViewModel ViewModel{get;set;}

        public Boolean Connected{get;set;}=false;
        
        public UdpSocketClient(ViewModel.MainWindowViewModel _ViewModel) {
            this.ViewModel=_ViewModel;
            this.AsyncNetUdpClient=new AsyncNetUdpClient(_ViewModel.Address,_ViewModel.Port);
            this.AsyncNetUdpClient.ClientExceptionOccured+=this.AsyncNetUdpClient_ClientExceptionOccured;
            this.AsyncNetUdpClient.ClientReady+=this.AsyncNetUdpClient_ClientReady;
            this.AsyncNetUdpClient.ClientStarted+=this.AsyncNetUdpClient_ClientStarted;
            this.AsyncNetUdpClient.ClientStopped+=this.AsyncNetUdpClient_ClientStopped;
            this.AsyncNetUdpClient.UdpSendErrorOccured+=this.AsyncNetUdpClient_UdpSendErrorOccured;
            this.AsyncNetUdpClient.UdpPacketArrived+=this.AsyncNetUdpClient_UdpPacketArrived;
            this.AsyncNetUdpClient.StartAsync();
        }

        /// <summary>
        /// 套接字链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AsyncNetUdpClient_ClientStarted(object sender,UdpClientStartedEventArgs e)=>this.ViewModel.OutputString=$"套接字链接,{e.TargetHostname}:{e.TargetPort}";
        /// <summary>
        /// 套接字断开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AsyncNetUdpClient_ClientStopped(object sender,UdpClientStoppedEventArgs e){
            this.Connected=false;
            if(this.ViewModel!=null){this.ViewModel.OutputString="套接字断开";}
        }
        /// <summary>
        /// 套接字异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AsyncNetUdpClient_ClientExceptionOccured(object sender,UdpClientExceptionEventArgs e){
            if(this.ViewModel!=null){this.ViewModel.OutputString=$"套接字异常,{e.Exception.Message},{e.Exception.StackTrace}";}
        }
        /// <summary>
        /// 套接字发送数据时异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AsyncNetUdpClient_UdpSendErrorOccured(object sender,UdpSendErrorEventArgs e)=>this.ViewModel.OutputString=$"套接字发送数据时异常,{e.Exception}";
        /// <summary>
        /// 套接字等待中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AsyncNetUdpClient_ClientReady(object sender,UdpClientReadyEventArgs e){
            //this.Connected=true;
            this.ViewModel.OutputString="套接字等待中";
            this.PostSync("{\"ActionId\":1,\"ActionName\":\"Hello\"}");
        }
        /// <summary>
        /// 套接字接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AsyncNetUdpClient_UdpPacketArrived(object sender,UdpPacketArrivedEventArgs e) {
            this.ViewModel.ByteRecv+=e.PacketData.Length;
            if(!Helper.AesEncrypt.Decrypt(this.ViewModel.Key,e.PacketData,out String s1)){this.ViewModel.OutputString=$"套接字接收数据=>无法解析";return;}
            String json=s1.TrimEnd('\0');
            this.ViewModel.OutputString=$"套接字接收数据=>{json}";
            dynamic d1=Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            if(d1.ActionId==null || d1.ActionId<1 || d1.ErrorCode==null || d1.ErrorCode>0){return;}
            switch ((Int32)d1.ActionId) {
                case 1:this.Connected=true;this.ViewModel.Window.Dispatcher.InvokeAsync(()=>{ System.Windows.Input.CommandManager.InvalidateRequerySuggested();});break;
                case 1001:this.ViewModel.OutputString="指令已完成=>FetchUnits";break;
                case 1002:this.ViewModel.OutputString="指令已完成=>StartAllUnits";break;
                case 1003:this.ViewModel.OutputString="指令已完成=>StartUnit";break;
                case 1004:this.ViewModel.OutputString="指令已完成=>StopAllUnits";break;
                case 1005:this.ViewModel.OutputString="指令已完成=>StopUnit";break;
            }
        }

        public void PostSync(String _Text){
            if(!Helper.AesEncrypt.Encrypt(this.ViewModel.Key,_Text,out Byte[] data)){return;}
            this.AsyncNetUdpClient.Post(data);
            this.ViewModel.ByteSent+=data.Length;
            this.ViewModel.OutputString=$"套接字加入发送队列=>{_Text}";
        }

        public void SendAsync(String _Text) {
            if(!Helper.AesEncrypt.Encrypt(this.ViewModel.Key,_Text,out Byte[] data)){return;}
            this.AsyncNetUdpClient.Post(data);
            this.ViewModel.ByteSent+=data.Length;
            this.ViewModel.OutputString=$"套接字发送数据=>{_Text}";
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: 释放托管状态(托管对象)。
                    this.AsyncNetUdpClient.UdpClient.Close();
                    this.ViewModel.OutputString="套接字释放";
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                this.ViewModel=null;
                disposedValue=true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~UdpSocketClient() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose() {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
