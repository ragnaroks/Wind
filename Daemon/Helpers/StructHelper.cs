using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Daemon.Helpers {
    public static class StructHelper {
        public static Byte[] StructToBytes(Object structObject) {
            Int32 size=Marshal.SizeOf(structObject);
            Byte[] bytes=new Byte[size];
            IntPtr structPtr=Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(structObject,structPtr,false);
            Marshal.Copy(structPtr,bytes,0,size);
            Marshal.FreeHGlobal(structPtr);
            return bytes;
        }

        public static Object BytesToStruct(Byte[] structBytes,Type structType){
            Int32 size=Marshal.SizeOf(structType);
            if(size>structBytes.Length){return null;}
            IntPtr structPtr=Marshal.AllocHGlobal(size);
            Marshal.Copy(structBytes,0,structPtr,size);
            Object structObject=Marshal.PtrToStructure(structPtr,structType);
            Marshal.FreeHGlobal(structPtr);
            return structObject;
        }
    }
}
