using System;
using System.Collections.Generic;
using System.Text;

namespace Host.Helper {
    public static class Extend {
        /// <summary>
        /// 获取字符串的SHA1
        /// </summary>
        /// <param name="_this"></param>
        /// <param name="_UpCase">是否大写</param>
        /// <returns></returns>
        public static String SHA1(this String _this,Boolean _UpCase=false) {
            System.Security.Cryptography.SHA1 sha1=System.Security.Cryptography.SHA1.Create();
            Byte[] bytes=sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(_this));
            String hash=BitConverter.ToString(bytes).Replace("-","");
            return _UpCase?hash:hash.ToLower();
        }

        /// <summary>
        /// 获取字符串的MD5
        /// </summary>
        /// <param name="_this"></param>
        /// <param name="_UpCase">是否大写</param>
        /// <returns></returns>
        public static String MD5(this String _this,Boolean _UpCase=false) {
            System.Security.Cryptography.MD5 md5=System.Security.Cryptography.MD5.Create();
            Byte[] bytes=md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(_this));
            String hash=BitConverter.ToString(bytes).Replace("-","");
            return _UpCase?hash:hash.ToLower();
        }
    }
}
