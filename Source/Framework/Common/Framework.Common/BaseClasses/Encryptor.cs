using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Cedar.Framework.Common.BaseClasses
{
    /// <summary>
    ///     For 加密解密的常用算法,DES,MD5,SHA1,RSA,Rijndael,...此类不能被代码格式化;
    /// </summary>
    /// {Design BY:Tim, Use:Tim, For 加密解密算法,DES,MD5,SHA1,RSA,Rijndael,... }
    public class Encryptor
    {
        private const string MKeyIv = "Chinaccn"; /* 默认密钥,长度必须是8位 */
        private static readonly byte[] Keys = {0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF}; /* 默认密钥向量 */

        #region SHA1 加密(不可逆算法) ;

        /// <summary>
        ///     { For【安全哈希算法】SHA1有如下特性：不可以从消息摘要中复原信息；两个不同的消息不会产生同样的消息摘要 }
        /// </summary>
        /// <param name="encryptString">待加密字符</param>
        public string EncryptSha1(string encryptString)
        {
            var strRes = Encoding.Default.GetBytes(encryptString);
            HashAlgorithm iSha = new SHA1CryptoServiceProvider();
            strRes = iSha.ComputeHash(strRes);
            var enText = new StringBuilder();
            foreach (var iByte in strRes)
            {
                enText.AppendFormat("{0:x2}", iByte);
            }
            return enText.ToString();
        }

        #endregion

        /// <summary>
        ///     安全码认证
        /// </summary>
        /// <returns></returns>
        public string GetSecurity()
        {
            var strSec = "Chinaccn" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day;
            return MD5Encrypt(strSec).ToLower();
        }

        /// <summary>
        ///     MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string MD5Encrypt(string str)
        {
            var cl = str;
            var md5 = MD5.Create(); //实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            var s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            return s.Aggregate("", (current, t) => current + t.ToString("X2"));
        }

        #region DES (字符串)加密解密算法 ;

        /// <summary>
        ///     { Coder:Tim,At:[ 2015-09-23 17:27 ], For 使用默认密钥【DES加密】（重载） }
        /// </summary>
        public string EncryptDes(string encryptString)
        {
            return EncryptDes(encryptString, MKeyIv);
        }

        /// <summary>
        ///     { Coder:Tim,At:[ 2015-09-23 17:27 ], For 使用默认密钥【DES解密】（重载） }
        /// </summary>
        public string DecryptDes(string encryptString)
        {
            return DecryptDes(encryptString, MKeyIv);
        }

        /// <summary>
        ///     { Coder:Tim,At:[ 2015-09-23 17:27 ], For 【DES加密字符串】 }
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败以后返回源字符串</returns>
        public string EncryptDes(string encryptString, string encryptKey)
        {
            try
            {
                var rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                var rgbIv = Keys;
                var inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                var dCsp = new DESCryptoServiceProvider();
                var mStream = new MemoryStream();
                var cStream = new CryptoStream(mStream, dCsp.CreateEncryptor(rgbKey, rgbIv), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        ///     { Coder:Tim,At:[ 2015-09-23 17:27 ], For 【DES解密字符串】 }
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败以后返回源字符串</returns>
        public string DecryptDes(string decryptString, string decryptKey)
        {
            try
            {
                var rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                var rgbIv = Keys;
                var inputByteArray = Convert.FromBase64String(decryptString);
                var dcsp = new DESCryptoServiceProvider();
                var mStream = new MemoryStream();
                var cStream = new CryptoStream(mStream, dcsp.CreateDecryptor(rgbKey, rgbIv), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        #endregion

        #region MD5 加密算法(不可逆算法) ;

        /// <summary>
        ///     { Coder:Tim,At:[ 2015-09-23 17:27 ], For 默认的MD5加密（返回16位） }
        /// </summary>
        /// <param name="inStr">inStr,要加密的字符串</param>
        /// <returns>16位，MD5加密后字符串，失败以后返回源字符串</returns>
        public string EncryptMd5(string inStr)
        {
            return Encrypt16Md5(inStr);
        }

        /// <summary>
        ///     { Coder:Tim,At:[ 2015-09-23 17:27 ],For 【MD5加密返回16位】 }
        /// </summary>
        /// <param name="inStr">inStr,要加密的字符串</param>
        /// <returns>16位，MD5加密后字符串，失败以后返回源字符串</returns>
        public string Encrypt16Md5(string inStr)
        {
            try
            {
                var md5 = new MD5CryptoServiceProvider();
                var t2 = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(inStr)), 4, 8);
                t2 = t2.Replace("-", "");
                return t2;
            }
            catch
            {
                return inStr;
            }
        }

        /// <summary>
        ///     { Coder:Tim,At:[ 2015-09-23 17:27 ],For 【MD5加密返回32位】 }
        /// </summary>
        /// <param name="inStr">inStr,要加密的字符串</param>
        /// <returns>32位，MD5加密后字符串，失败以后返回源字符串</returns>
        public string Encrypt32Md5(string inStr)
        {
            try
            {
                /* 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择
                 * 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
                 * 循环里,将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                 */
                var cl = inStr;
                var md5 = MD5.Create();
                var s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
                return s.Aggregate("", (current, t) => current + t.ToString("X"));
            }
            catch
            {
                return inStr;
            }
        }

        #endregion

        #region DES (url,id)加密解密算法 ;

        /// <summary>
        ///     { Coder:Tim,At:[ 2015-09-23 17:27 ], For 使用默认密钥【对url或id加密】（重载） }
        /// </summary>
        public string EncryptId(string encryptString)
        {
            return EncryptId(encryptString, MKeyIv);
        }

        /// <summary>
        ///     { Coder:Tim,At:[ 2015-09-23 17:27 ], For 使用默认密钥【对url或id解密】（重载） }
        /// </summary>
        public string DecryptId(string encryptString)
        {
            return DecryptId(encryptString, MKeyIv);
        }

        /// <summary>
        ///     { Coder:Tim,At:[ 2015-09-23 17:27 ], For 【DES加密url或id】 }
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败以后返回源字符串</returns>
        public string EncryptId(string encryptString, string encryptKey)
        {
            try
            {
                var des = new DESCryptoServiceProvider
                {
                    IV = Keys,
                    Key = Encoding.ASCII.GetBytes(encryptKey)
                };

                var inputByteArray = Encoding.Default.GetBytes(encryptString);
                var ms = new MemoryStream(); /* 创建其支持存储区为内存的流  */
                var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write); /* 定义将数据流链接到加密转换的流  */
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock(); /* 已经完成了把加密后的结果放到内存中去  */
                var ret = new StringBuilder();
                foreach (var b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                return ret.ToString();
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        ///     { Coder:Tim,At:[ 2015-09-23 17:27 ], For 【DES解密url或id】 }
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败以后返回源字符串</returns>
        public string DecryptId(string decryptString, string decryptKey)
        {
            try
            {
                var des = new DESCryptoServiceProvider
                {
                    IV = Keys,
                    Key = Encoding.ASCII.GetBytes(decryptKey)
                };
                /* 建立加密对象的密钥和偏移量，此值重要，不能修改 */

                var inputByteArray = new byte[decryptString.Length/2];
                for (var x = 0; x < decryptString.Length/2; x++)
                {
                    var i = (Convert.ToInt32(decryptString.Substring(x*2, 2), 16));
                    inputByteArray[x] = (byte) i;
                }
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Encoding.Default.GetString(ms.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        #endregion

        #region AES加密

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="plainStr"></param>
        /// <returns></returns>
        public static string EncryptAes(string plainStr)
        {
            var md5Password = GenerateHash(MKeyIv);
            var md5Vi = GenerateHash(MKeyIv + md5Password).Substring(0, 16);
            
            var bKey = Encoding.UTF8.GetBytes(md5Password);
            var bIv = Encoding.UTF8.GetBytes(md5Vi);
            var byteArray = Encoding.UTF8.GetBytes(plainStr);
            
            string encrypt = null;
            var aes = Rijndael.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 256;
            try
            {
                using (var mStream = new MemoryStream())
                {
                    using (var cStream = new CryptoStream(mStream, aes.CreateEncryptor(bKey, bIv), CryptoStreamMode.Write))
                    {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        encrypt = Convert.ToBase64String(mStream.ToArray());
                    }
                }
            }
            catch
            {
                // ignored
            }
            aes.Clear();
            return encrypt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePathAndName"></param>
        /// <returns></returns>
        public static string GenerateHash(string filePathAndName)
        {
            var hashData = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(filePathAndName)); // MD5
            return hashData.Select(b => b.ToString("X").ToLower()).Aggregate("", (current, hexValue) => current + ((hexValue.Length == 1 ? "0" : "") + hexValue));
        }

        #endregion
    }
}