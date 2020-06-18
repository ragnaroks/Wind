using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace wind.Helpers {
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

        public static Int64 ToLocalTimestamp(this DateTime value){
            DateTime origin=new DateTime(1970,1,1,0,0,0,DateTimeKind.Local);
            return (Int64)(value-origin).TotalSeconds;
        }
    }
}
