using System;

namespace windctl.Entities.Common {
    public class AppSettings {
        public String RemoteControlAddress{get;set;}=null;
        public Int32 RemoteControlPort{get;set;}=0;
        public String RemoteControlKey{get;set;}=null;
    }
}
