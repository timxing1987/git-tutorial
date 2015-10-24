using System;
using System.Configuration;
using System.Text;
using System.Web;
using PostMsg_Net;
using PostMsg_Net.common;

namespace Cedar.Foundation.SMS.Common
{
    /// <summary>
    /// </summary>
    public class SMSMSG
    {
        private readonly PostMsg _postMsg;
        private readonly string _smsapi = ConfigurationManager.AppSettings["smsapi"];

        /// <summary>
        /// </summary>
        public SMSMSG()
        {
            var username = "chexin@chexin";
            var password = "J2!*KL%[";
            var postUp = "211.147.239.62:9070";
            var postDl = "211.147.239.62:9080";

            if (ConfigurationManager.AppSettings["sms_username"] != null)
            {
                username = ConfigurationManager.AppSettings["sms_username"];
            }
            if (ConfigurationManager.AppSettings["sms_password"] != null)
            {
                password = ConfigurationManager.AppSettings["sms_password"];
            }
            if (ConfigurationManager.AppSettings["sms_post_up"] != null)
            {
                postUp = ConfigurationManager.AppSettings["sms_post_up"];
            }
            if (ConfigurationManager.AppSettings["sms_post_dl"] != null)
            {
                postDl = ConfigurationManager.AppSettings["sms_post_dl"];
            }

            _postMsg = new PostMsg();
            //设置账号
            _postMsg.SetUser(username, password);
            //设置上行网关
            _postMsg.SetMOAddress(postUp.Split(':')[0], Convert.ToInt32(postUp.Split(':')[1]), LinkType.SHORTLINK);
            //设置下行网关
            _postMsg.SetGateWay(postDl.Split(':')[0], Convert.ToInt32(postDl.Split(':')[1]), LinkType.SHORTLINK);
        }

        /// <summary>
        ///     http接口发送
        /// </summary>
        /// <param name="to">发送对象</param>
        /// <param name="content">发送内容</param>
        public SendResult SendSms(string to, string content)
        {
            var newurl = string.Format(_smsapi, to, HttpUtility.UrlEncode(content, Encoding.GetEncoding("GBK")));
            var sendResult = SMSSDK.GetWebRequest(newurl);
            try
            {
                return new SendResult
                {
                    errcode = sendResult.Split(',')[0],
                    errmsg = SMSSDK.DicSmsResultInfo[sendResult.Split(',')[0]]
                };
            }
            catch (Exception)
            {
                return new SendResult
                {
                    errcode = "-100",
                    errmsg = SMSSDK.DicSmsResultInfo[sendResult.Split(',')[0]]
                };
            }
        }

        /// <summary>
        ///     后台接口发送
        /// </summary>
        /// <param name="to">发送对象</param>
        /// <param name="content">发送内容</param>
        /// <returns></returns>
        public SendResult PostSms(string to, string content)
        {
            //短信单发，返回发送结果
            var result = _postMsg.Post(_postMsg.GetAccount(), to, content, string.Empty);
            try
            {
                return new SendResult
                {
                    errcode = result.ToString(),
                    errmsg = SMSSDK.DicSmsResultInfo[result.ToString()]
                };
            }
            catch (Exception)
            {
                return new SendResult
                {
                    errcode = "-100",
                    errmsg = SMSSDK.DicSmsResultInfo[result.ToString()]
                };
            }
        }
    }

    /// <summary>
    /// </summary>
    public class SendResult
    {
        /// <summary>
        /// </summary>
        public string errcode { get; set; }

        /// <summary>
        /// </summary>
        public string errmsg { get; set; }
    }
}