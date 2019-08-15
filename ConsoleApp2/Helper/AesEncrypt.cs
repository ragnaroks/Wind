using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp2.Helper {
    public class AesEncrypt {
        private static readonly Byte[] IV=new Byte[16]{55,51,48,99,115,103,111,55,51,53,53,54,48,56,99,52};//"730csgo7355608c4"
        
        public static Byte[] Encrypt(String _key,String _text) {
            var aes=AesManaged.Create();
            //aes.IV=IV;
            aes.BlockSize=128;
            aes.KeySize=128;
            aes.Key=Encoding.UTF8.GetBytes(_key.MD5().Substring(16));
            aes.Mode=CipherMode.ECB;
            aes.Padding=PaddingMode.Zeros;
            ICryptoTransform crypto=aes.CreateEncryptor(aes.Key,aes.IV);
            Byte[] _bytes = Encoding.UTF8.GetBytes(_text);
            Byte[] result=crypto.TransformFinalBlock(_bytes,0,_bytes.Length);
            crypto.Dispose();
            aes.Dispose();
            return result;
        }

        public static String Decrypt(String _key,Byte[] _bytes) {
            var aes=AesManaged.Create();
            //aes.IV=IV;
            aes.BlockSize=128;
            aes.KeySize=128;
            aes.Key=Encoding.UTF8.GetBytes(_key.MD5().Substring(16));
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
