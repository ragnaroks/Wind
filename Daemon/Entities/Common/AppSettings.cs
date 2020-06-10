using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Daemon.Entities.Common {
    public class AppSettings {
        public String ControlListenAddress{get;private set;}=null;
        public Int32 ControlListenPort{get;private set;}=0;
        public String ControlKey{get;private set;}=null;

        public Boolean Setup(IConfiguration configuration) {
            this.ControlListenAddress=configuration.GetValue<String>("ControlListenAddress",null);
            this.ControlListenPort=configuration.GetValue<Int32>("ControlListenPort",0);
            this.ControlKey=configuration.GetValue<String>("ControlKey",null);

            if(!this.ValidateControlListenAddress()){return false;}
            if(!this.ValidateControlListenPort()){return false;}
            if(!this.ValidateControlKey()){return false;}

            return true;
        }

        private Boolean ValidateControlListenAddress() {
            if(String.IsNullOrWhiteSpace(this.ControlListenAddress)){return false;}
            if(this.ControlListenAddress=="localhost"){return true;}
            Regex regex=new Regex(@"^[0-9\.]{7,15}$",RegexOptions.Compiled);
            if(!regex.IsMatch(this.ControlListenAddress)){return false;}
            return true;
        }

        private Boolean ValidateControlListenPort() {
            if(this.ControlListenPort<1024 || this.ControlListenPort>65535){return false;}
            return true;
        }

        private Boolean ValidateControlKey() {
            if(String.IsNullOrWhiteSpace(this.ControlKey)){return false;}
            Regex regex=new Regex(@"^\S{8,128}$",RegexOptions.Compiled);
            if(!regex.IsMatch(this.ControlKey)){return false;}
            return true;
        }
    }
}
