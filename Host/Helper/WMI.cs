using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace Host.Helper {
    public class WMI {
        public static Int64 GetPhysicalMemorySize() {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(); 
            searcher.Query = new SelectQuery("Win32_PhysicalMemory","",new String[]{"Capacity"});
            ManagementObjectCollection collection = searcher.Get();
            ManagementObjectCollection.ManagementObjectEnumerator em = collection.GetEnumerator();
            Int64 capacity = 0;
            while(em.MoveNext()){
                ManagementBaseObject baseObj = em.Current;
                if(baseObj.Properties["Capacity"]==null){continue;}
                Int64.TryParse(baseObj.Properties["Capacity"].Value.ToString(),out Int64 i1);
                capacity+=i1;
            }
            return capacity;
        }
    }
}
