using System;
using System.Security.Cryptography;
using System.Text;

namespace AesEncryptor {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            while (true) {
                String input=Console.ReadLine();
                Byte[] output=Encrypt(input);
                StringBuilder sb1=new StringBuilder();
                for(Int32 i = 0;i<output.Length;i++) {
                    sb1.Append(output[i].ToString("x2"));
                }
                Console.WriteLine(sb1+"=>"+Encoding.UTF8.GetString(output));
            }
        }

        public static Byte[] Encrypt(String _text) {
            var aes=AesManaged.Create();
            aes.BlockSize=128;
            aes.KeySize=128;
            aes.Key=new Byte[16]{51,52,49,49,57,98,100,102,99,48,55,52,54,49,48,102};
            aes.Mode=CipherMode.ECB;
            aes.Padding=PaddingMode.Zeros;
            ICryptoTransform crypto=aes.CreateEncryptor(aes.Key,aes.IV);
            Byte[] _bytes = Encoding.UTF8.GetBytes(_text);
            Byte[] result=crypto.TransformFinalBlock(_bytes,0,_bytes.Length);
            crypto.Dispose();
            aes.Dispose();
            return result;
        }
    }
}
