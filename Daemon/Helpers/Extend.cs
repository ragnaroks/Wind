using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Daemon.Helpers {
    public static class Extend {
        public static T DeepClone<T>(this T value) where T : class {
            MemoryStream ms=new MemoryStream();
            BinaryFormatter formatter=new BinaryFormatter();
            formatter.Serialize(ms, value);
            ms.Position=0;
            T cloneObject=formatter.Deserialize(ms) as T;
            ms.Dispose();
            return cloneObject;
        }

        public static String FixedByteSize(this Int64 value){
            if(value<1){return "0 B";}
            if(value>0 && value<1024){return value+" B";}
            if(value>=1024 && value<1048576){
                return String.Concat(String.Format(CultureInfo.InvariantCulture,"{0:N2}",value/1024).Replace(".00","",StringComparison.OrdinalIgnoreCase)," KiB");
            }
            if(value>=1048576 && value<1073741824){
                return String.Concat(String.Format(CultureInfo.InvariantCulture,"{0:N2}",value/1048576).Replace(".00","",StringComparison.OrdinalIgnoreCase)," MiB");
            }
            return String.Concat(String.Format(CultureInfo.InvariantCulture,"{0:N2}",value/1073741824).Replace(".00","",StringComparison.OrdinalIgnoreCase)," GiB");
        }

        public static String FixedByteSize(this Single value){
            if(value<1){return "0 B";}
            if(value>0 && value<1024){return value+" B";}
            if(value>=1024 && value<1048576){
                return String.Concat(String.Format(CultureInfo.InvariantCulture,"{0:N2}",value/1024).Replace(".00","",StringComparison.OrdinalIgnoreCase)," KiB");
            }
            if(value>=1048576 && value<1073741824){
                return String.Concat(String.Format(CultureInfo.InvariantCulture,"{0:N2}",value/1048576).Replace(".00","",StringComparison.OrdinalIgnoreCase)," MiB");
            }
            return String.Concat(String.Format(CultureInfo.InvariantCulture,"{0:N2}",value/1073741824).Replace(".00","",StringComparison.OrdinalIgnoreCase)," GiB");
        }

        public static String FixedByteSize(this Double value){
            if(value<1){return "0 B";}
            if(value>0 && value<1024){return value+" B";}
            if(value>=1024 && value<1048576){
                return String.Concat(String.Format(CultureInfo.InvariantCulture,"{0:N2}",value/1024).Replace(".00","",StringComparison.OrdinalIgnoreCase)," KiB");
            }
            if(value>=1048576 && value<1073741824){
                return String.Concat(String.Format(CultureInfo.InvariantCulture,"{0:N2}",value/1048576).Replace(".00","",StringComparison.OrdinalIgnoreCase)," MiB");
            }
            return String.Concat(String.Format(CultureInfo.InvariantCulture,"{0:N2}",value/1073741824).Replace(".00","",StringComparison.OrdinalIgnoreCase)," GiB");
        }
    }
}
