using System;
using System.Collections.Generic;
using System.Text;

namespace Daemon.Helpers {
    public static class Extend {
        /// <summary>
        /// 获取字符串的SHA1
        /// </summary>
        /// <param name="thisString"></param>
        /// <param name="upCase">是否大写</param>
        /// <returns></returns>
        public static String SHA1(this String thisString,Boolean upCase=false) {
            System.Security.Cryptography.SHA1 sha1=System.Security.Cryptography.SHA1.Create();
            Byte[] bytes=sha1.ComputeHash(Encoding.UTF8.GetBytes(thisString));
            String hash=BitConverter.ToString(bytes).Replace("-","");
            return upCase?hash:hash.ToLower();
        }

        /// <summary>
        /// 获取字符串的MD5
        /// </summary>
        /// <param name="thisString"></param>
        /// <param name="upCase">是否大写</param>
        /// <returns></returns>
        public static String MD5(this String thisString,Boolean upCase=false) {
            System.Security.Cryptography.MD5 md5=System.Security.Cryptography.MD5.Create();
            Byte[] bytes=md5.ComputeHash(Encoding.UTF8.GetBytes(thisString));
            String hash=BitConverter.ToString(bytes).Replace("-","");
            return upCase?hash:hash.ToLower();
        }
    }
}
