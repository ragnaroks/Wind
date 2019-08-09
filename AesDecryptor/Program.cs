using System;
using System.Security.Cryptography;
using System.Text;

namespace AesDecryptor {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            while (true) {
                String input=Console.ReadLine();
                Byte[] bytes=new Byte[input.Length/2];
                for (int i = 0; i < input.Length; i += 2){
                    bytes[i / 2] = (byte)Convert.ToByte(input.Substring(i, 2), 16);
                }
                String s1=Decrypt(bytes);
                Console.WriteLine(input+"=>"+s1);
            }
        }

        public static String Decrypt(Byte[] _bytes) {
            var aes=AesManaged.Create();
            aes.BlockSize=128;
            aes.KeySize=128;
            aes.Key=new Byte[16]{51,52,49,49,57,98,100,102,99,48,55,52,54,49,48,102};
            aes.Mode=CipherMode.ECB;
            aes.Padding=PaddingMode.Zeros;
            ICryptoTransform crypto=aes.CreateDecryptor(aes.Key,aes.IV);
            Byte[] result=null;
            try {
                result=crypto.TransformFinalBlock(_bytes,0,_bytes.Length);
            }catch(Exception _e) {
                crypto.Dispose();
                aes.Dispose();
                return null;
            }
            crypto.Dispose();
            aes.Dispose();
            return Encoding.UTF8.GetString(result);
        }
    }
}
