/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
 
    �ļ�����TenPayUtil.cs
    �ļ�����������΢��֧�������ļ�
    
    
    ������ʶ��Senparc - 20150722
----------------------------------------------------------------*/

using System;
using System.Text;
using Senparc.Weixin.QY.Helpers;

namespace Senparc.Weixin.QY.TenPayLib
{
    /// <summary>
    ///     TenpayUtil ��ժҪ˵����
    ///     �����ļ�
    /// </summary>
    public class TenPayUtil
    {
        /// <summary>
        ///     �������Noncestr
        /// </summary>
        /// <returns></returns>
        public static string GetNoncestr()
        {
            var random = new Random();
            return MD5UtilHelper.GetMD5(random.Next(1000).ToString(), "GBK");
        }

        public static string GetTimestamp()
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        ///     ���ַ�������URL����
        /// </summary>
        /// <param name="instr"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string UrlEncode(string instr, string charset)
        {
            //return instr;
            if (instr == null || instr.Trim() == "")
                return "";
            string res;

            try
            {
                res = System.Web.HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));
            }
            catch (Exception ex)
            {
                res = System.Web.HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
            }


            return res;
        }

        /// <summary>
        ///     ���ַ�������URL����
        /// </summary>
        /// <param name="instr"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string UrlDecode(string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            string res;

            try
            {
                res = System.Web.HttpUtility.UrlDecode(instr, Encoding.GetEncoding(charset));
            }
            catch (Exception ex)
            {
                res = System.Web.HttpUtility.UrlDecode(instr, Encoding.GetEncoding("GB2312"));
            }


            return res;
        }


        /// <summary>
        ///     ȡʱ��������漴��,�滻���׵����еĺ�10λ��ˮ��
        /// </summary>
        /// <returns></returns>
        public static uint UnixStamp()
        {
            var ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return Convert.ToUInt32(ts.TotalSeconds);
        }

        /// <summary>
        ///     ȡ�����
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string BuildRandomStr(int length)
        {
            var rand = new Random();

            var num = rand.Next();

            var str = num.ToString();

            if (str.Length > length)
            {
                str = str.Substring(0, length);
            }
            else if (str.Length < length)
            {
                var n = length - str.Length;
                while (n > 0)
                {
                    str.Insert(0, "0");
                    n--;
                }
            }

            return str;
        }
    }
}