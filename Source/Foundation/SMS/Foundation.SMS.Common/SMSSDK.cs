using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace Cedar.Foundation.SMS.Common
{
    public class SMSSDK
    {
        /// <summary>
        /// </summary>
        public static readonly Dictionary<string, string> DicSmsResultInfo = new Dictionary<string, string>
        {
            {"0", "成功"},
            {"-1", "账号无效"},
            {"-2", "参数：无效"},
            {"-3", "连接不上服务器"},
            {"-5", "无效的短信数据，号码格式不对"},
            {"-6", "用户名密码错误"},
            {"-7", "旧密码不正确"},
            {"-9", "资金账户不存在"},
            {"-11", "包号码数量超过最大限制"},
            {"-12", "余额不足"},
            {"-99", "系统内部错误"},
            {"-100", "其它错误"}
        };

        private string _smsapi = ConfigurationManager.AppSettings["smsapi"];

        /// <summary>
        ///     请求URL（以GET方式请求）
        /// </summary>
        /// <param name="postUrl">请求地址</param>
        /// <returns>请求结果</returns>
        public static string GetWebRequest(string postUrl)
        {
            string ret;
            try
            {
                var webReq = (HttpWebRequest) WebRequest.Create(new Uri(postUrl));
                webReq.Method = "Get";
                webReq.ContentType = "application/x-www-form-urlencoded";
                var response = (HttpWebResponse) webReq.GetResponse();
                // ReSharper disable once AssignNullToNotNullAttribute
                var sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                return "-1";
            }
            return ret;
        }

        /// <summary>
        ///     请求URL（以GET方式请求）
        /// </summary>
        /// <param name="postUrl">请求地址</param>
        /// <returns>请求结果</returns>
        public static string GetWebRequest2(string postUrl)
        {
            //创建请求
            var request = (HttpWebRequest) WebRequest.Create(new Uri(postUrl));

            //GET请求
            request.Method = "GET";
            request.ReadWriteTimeout = 5000;
            request.ContentType = "text/html;charset=UTF-8";
            var response = (HttpWebResponse) request.GetResponse();
            var myResponseStream = response.GetResponseStream();
            if (myResponseStream != null)
            {
                var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

                //返回内容
                var retString = myStreamReader.ReadToEnd();
                return retString;
            }
            return "-1";
        }

        /// <summary>
        ///     请求URL（以POST方式请求）
        /// </summary>
        /// <param name="postUrl">请求地址</param>
        /// <param name="param"></param>
        /// <returns>请求结果</returns>
        public static string PostWebRequest(string postUrl, string param)
        {
            var req = (HttpWebRequest) WebRequest.Create(postUrl);
            var encoding = Encoding.UTF8;
            var bs = Encoding.ASCII.GetBytes(param);
            string responseData;
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = bs.Length;
            using (var reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
                reqStream.Close();
            }
            using (var response = (HttpWebResponse) req.GetResponse())
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                using (var reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    responseData = reader.ReadToEnd();
                }
            }

            return responseData;
        }
    }
}