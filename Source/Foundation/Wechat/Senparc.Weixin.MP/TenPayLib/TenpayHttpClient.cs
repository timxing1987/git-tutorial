/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
 
    文件名：TenPayHttpClient.cs
    文件功能描述：微信支付http、https通信类
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

/**
 * http、https通信类
 * ============================================================================
 * api说明：
 * setReqContent($reqContent),设置请求内容，无论post和get，都用get方式提供
 * getResContent(), 获取应答内容
 * setMethod($method),设置请求方法,post或者get
 * getErrInfo(),获取错误信息
 * setCertInfo($certFile, $certPasswd, $certType="PEM"),设置证书，双向https时需要使用
 * setCaInfo($caFile), 设置CA，格式未pem，不设置则不检查
 * setTimeOut($timeOut)， 设置超时时间，单位秒
 * getResponseCode(), 取返回的http状态码
 * call(),真正调用接口
 * 
 * ============================================================================
 *
 */

namespace Senparc.Weixin.MP.TenPayLib
{
    public class TenPayHttpClient
    {
        /// <summary>
        ///     ca证书文件
        /// </summary>
        private string CaFile;

        /// <summary>
        ///     证书文件
        /// </summary>
        private string CertFile;

        /// <summary>
        ///     证书密码
        /// </summary>
        private string CertPasswd;

        /// <summary>
        ///     字符编码
        /// </summary>
        private string Charset;

        /// <summary>
        ///     错误信息
        /// </summary>
        private string ErrInfo;

        /// <summary>
        ///     请求方法
        /// </summary>
        private string Method;

        /// <summary>
        ///     请求内容，无论post和get，都用get方式提供
        /// </summary>
        private string ReqContent;

        /// <summary>
        ///     应答内容
        /// </summary>
        private string ResContent;

        /// <summary>
        ///     http应答编码
        /// </summary>
        private int ResponseCode;

        /// <summary>
        ///     超时时间,以秒为单位
        /// </summary>
        private int TimeOut;

        public TenPayHttpClient()
        {
            CaFile = "";
            CertFile = "";
            CertPasswd = "";

            ReqContent = "";
            ResContent = "";
            Method = "POST";
            ErrInfo = "";
            TimeOut = 1*60; //5分钟

            ResponseCode = 0;
            Charset = "gb2312";
        }

        /// <summary>
        ///     设置请求内容
        /// </summary>
        /// <param name="reqContent"></param>
        public void SetReqContent(string reqContent)
        {
            ReqContent = reqContent;
        }

        /// <summary>
        ///     获取结果内容
        /// </summary>
        /// <returns></returns>
        public string GetResContent()
        {
            return ResContent;
        }

        /// <summary>
        ///     设置请求方法post或者get
        /// </summary>
        /// <param name="method"></param>
        public void SetMethod(string method)
        {
            Method = method;
        }

        /// <summary>
        ///     获取错误信息
        /// </summary>
        /// <returns></returns>
        public string GetErrInfo()
        {
            return ErrInfo;
        }

        /// <summary>
        ///     设置证书信息
        /// </summary>
        /// <param name="certFile"></param>
        /// <param name="certPasswd"></param>
        public void SetCertInfo(string certFile, string certPasswd)
        {
            CertFile = certFile;
            CertPasswd = certPasswd;
        }

        /// <summary>
        ///     设置ca
        /// </summary>
        /// <param name="caFile"></param>
        public void SetCaInfo(string caFile)
        {
            CaFile = caFile;
        }

        /// <summary>
        ///     设置超时时间,以秒为单位
        /// </summary>
        /// <param name="timeOut"></param>
        public void SetTimeOut(int timeOut)
        {
            TimeOut = timeOut;
        }


        /// <summary>
        ///     获取http状态码
        /// </summary>
        /// <returns></returns>
        public int GetResponseCode()
        {
            return ResponseCode;
        }

        public void SetCharset(string charset)
        {
            Charset = charset;
        }

        /// <summary>
        ///     验证服务器证书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary>
        ///     执行http调用
        /// </summary>
        /// <returns></returns>
        public bool Call()
        {
            StreamReader sr = null;
            HttpWebResponse wr = null;

            HttpWebRequest hp = null;
            try
            {
                string postData = null;
                if (Method.ToUpper() == "POST")
                {
                    var sArray = Regex.Split(ReqContent, "\\?");

                    hp = (HttpWebRequest) WebRequest.Create(sArray[0]);

                    if (sArray.Length >= 2)
                    {
                        postData = sArray[1];
                    }
                }
                else
                {
                    hp = (HttpWebRequest) WebRequest.Create(ReqContent);
                }


                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                if (CertFile != "")
                {
                    hp.ClientCertificates.Add(new X509Certificate2(CertFile, CertPasswd));
                }
                hp.Timeout = TimeOut*1000;

                var encoding = Encoding.GetEncoding(Charset);
                if (postData != null)
                {
                    var data = encoding.GetBytes(postData);

                    hp.Method = "POST";

                    hp.ContentType = "application/x-www-form-urlencoded";

                    hp.ContentLength = data.Length;

                    var ws = hp.GetRequestStream();

                    // 发送数据

                    ws.Write(data, 0, data.Length);
                    ws.Close();
                }


                wr = (HttpWebResponse) hp.GetResponse();
                sr = new StreamReader(wr.GetResponseStream(), encoding);


                ResContent = sr.ReadToEnd();
                sr.Close();
                wr.Close();
            }
            catch (Exception exp)
            {
                ErrInfo += exp.Message;
                if (wr != null)
                {
                    ResponseCode = Convert.ToInt32(wr.StatusCode);
                }

                return false;
            }

            ResponseCode = Convert.ToInt32(wr.StatusCode);

            return true;
        }
    }
}