using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace windctl.Helpers {
    public static class Extend {
        public static String FixedByteSize(this Single value){
            if(value<1){return "0 B";}
            if(value<1024){
                return String.Concat(String.Format(CultureInfo.InvariantCulture,"{0:N2}",value).Replace(".00","",StringComparison.OrdinalIgnoreCase)," B");
            }
            if(value<1048576){
                return String.Concat(String.Format(CultureInfo.InvariantCulture,"{0:N2}",value/1024).Replace(".00","",StringComparison.OrdinalIgnoreCase)," KiB");
            }
            if(value<1073741824){
                return String.Concat(String.Format(CultureInfo.InvariantCulture,"{0:N2}",value/1048576).Replace(".00","",StringComparison.OrdinalIgnoreCase)," MiB");
            }
            return String.Concat(String.Format(CultureInfo.InvariantCulture,"{0:N2}",value/1073741824).Replace(".00","",StringComparison.OrdinalIgnoreCase)," GiB");
        }

        public static String FixedByteSize(this Int64 value){
            if(value<1){return "0 B";}
            if(value<1024){
                return String.Concat(String.Format(CultureInfo.InvariantCulture,"{0:N2}",value).Replace(".00","",StringComparison.OrdinalIgnoreCase)," B");
            }
            if(value<1048576){
                return String.Concat(String.Format(CultureInfo.InvariantCulture,"{0:N2}",value/1024).Replace(".00","",StringComparison.OrdinalIgnoreCase)," KiB");
            }
            if(value<1073741824){
                return String.Concat(String.Format(CultureInfo.InvariantCulture,"{0:N2}",value/1048576).Replace(".00","",StringComparison.OrdinalIgnoreCase)," MiB");
            }
            return String.Concat(String.Format(CultureInfo.InvariantCulture,"{0:N2}",value/1073741824).Replace(".00","",StringComparison.OrdinalIgnoreCase)," GiB");
        }

        public static String ToLocalTimestampString(this Int64 value){
            if(value<1){return "-";}
            DateTime origin=new DateTime(1970,1,1,0,0,0,DateTimeKind.Local);
            return origin.AddSeconds(value).ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
