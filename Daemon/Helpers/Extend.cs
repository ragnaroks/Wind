using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Daemon.Helpers {
    public static class Extend {
        public static T DeepClone<T>(this T thisObject) where T : class {
            MemoryStream ms=new MemoryStream();
            BinaryFormatter formatter=new BinaryFormatter();
            formatter.Serialize(ms, thisObject);
            ms.Position=0;
            T cloneObject=formatter.Deserialize(ms) as T;
            ms.Dispose();
            return cloneObject;
        }
    }
}
