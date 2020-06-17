using System;

namespace wind.Entities.Common {
    public class AppSettings {
        public Boolean EnableRemoteControl{get;set;}=false;
        public String RemoteControlListenAddress{get;set;}=null;
        public Int32 RemoteControlListenPort{get;set;}=0;
        public String RemoteControlKey{get;set;}=null;
    }
}
