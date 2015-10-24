/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
 
    �ļ�����RequestHandler.cs
    �ļ�����������΢��֧�� ������
    
    
    ������ʶ��Senparc - 20150211
    
    �޸ı�ʶ��Senparc - 20150303
    �޸�����������ӿ�
----------------------------------------------------------------*/

using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.TenPayLib
{
    /**
    'ǩ��������
     ============================================================================/// <summary>
    'api˵����
    'Init();
    '��ʼ��������Ĭ�ϸ�һЩ������ֵ��
    'SetKey(key_)'�����̻���Կ
    'CreateMd5Sign(signParams);�ֵ�����Md5ǩ��
    'GenPackage(packageParams);��ȡpackage��
    'CreateSHA1Sign(signParams);����ǩ��SHA1
    'ParseXML();���xml
    'GetDebugInfo(),��ȡdebug��Ϣ
     * 
     * ============================================================================
     */

    public class RequestHandler
    {
        /// <summary>
        ///     debug��Ϣ
        /// </summary>
        private string DebugInfo;

        protected HttpContext HttpContext;

        /// <summary>
        ///     ��Կ
        /// </summary>
        private string Key;

        /// <summary>
        ///     ����Ĳ���
        /// </summary>
        protected Hashtable Parameters;

        public RequestHandler(HttpContext httpContext)
        {
            Parameters = new Hashtable();

            HttpContext = httpContext ?? HttpContext.Current;
        }

        /// <summary>
        ///     ��ʼ������
        /// </summary>
        public virtual void Init()
        {
        }

        /// <summary>
        ///     ��ȡdebug��Ϣ
        /// </summary>
        /// <returns></returns>
        public string GetDebugInfo()
        {
            return DebugInfo;
        }

        /// <summary>
        ///     ��ȡ��Կ
        /// </summary>
        /// <returns></returns>
        public string GetKey()
        {
            return Key;
        }

        /// <summary>
        ///     ������Կ
        /// </summary>
        /// <param name="key"></param>
        public void SetKey(string key)
        {
            Key = key;
        }

        /// <summary>
        ///     ���ò���ֵ
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="parameterValue"></param>
        public void SetParameter(string parameter, string parameterValue)
        {
            if (parameter != null && parameter != "")
            {
                if (Parameters.Contains(parameter))
                {
                    Parameters.Remove(parameter);
                }

                Parameters.Add(parameter, parameterValue);
            }
        }


        /// <summary>
        ///     ��ȡpackage��������ǩ����
        /// </summary>
        /// <returns></returns>
        public string GetRequestURL()
        {
            CreateSign();
            var sb = new StringBuilder();
            var akeys = new ArrayList(Parameters.Keys);
            akeys.Sort();
            foreach (string k in akeys)
            {
                var v = (string) Parameters[k];
                if (null != v && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + TenPayUtil.UrlEncode(v, GetCharset()) + "&");
                }
            }

            //ȥ�����һ��&
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }


            return sb.ToString();
        }

        /// <summary>
        ///     ����md5ժҪ,������:����������a-z����,������ֵ�Ĳ������μ�ǩ��
        /// </summary>
        protected virtual void CreateSign()
        {
            var sb = new StringBuilder();

            var akeys = new ArrayList(Parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                var v = (string) Parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append("key=" + GetKey());
            var sign = MD5UtilHelper.GetMD5(sb.ToString(), GetCharset()).ToUpper();

            SetParameter("sign", sign);

            //debug��Ϣ
            SetDebugInfo(sb + " => sign:" + sign);
        }


        /// <summary>
        ///     ����packageǩ��
        /// </summary>
        /// <returns></returns>
        public virtual string CreateMd5Sign()
        {
            var sb = new StringBuilder();
            var akeys = new ArrayList(Parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                var v = (string) Parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "".CompareTo(v) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }
            var sign = MD5UtilHelper.GetMD5(sb.ToString(), GetCharset()).ToLower();

            SetParameter("sign", sign);
            return sign;
        }


        /// <summary>
        ///     ����sha1ǩ��
        /// </summary>
        /// <returns></returns>
        public string CreateSHA1Sign()
        {
            var sb = new StringBuilder();
            var akeys = new ArrayList(Parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                var v = (string) Parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    if (sb.Length == 0)
                    {
                        sb.Append(k + "=" + v);
                    }
                    else
                    {
                        sb.Append("&" + k + "=" + v);
                    }
                }
            }
            var paySign = SHA1UtilHelper.GetSha1(sb.ToString()).ToLower();

            //debug��Ϣ
            SetDebugInfo(sb + " => sign:" + paySign);
            return paySign;
        }


        /// <summary>
        ///     ���XML
        /// </summary>
        /// <returns></returns>
        public string ParseXML()
        {
            var sb = new StringBuilder();
            sb.Append("<xml>");
            foreach (string k in Parameters.Keys)
            {
                var v = (string) Parameters[k];
                if (Regex.IsMatch(v, @"^[0-9.]$"))
                {
                    sb.Append("<" + k + ">" + v + "</" + k + ">");
                }
                else
                {
                    sb.Append("<" + k + "><![CDATA[" + v + "]]></" + k + ">");
                }
            }
            sb.Append("</xml>");
            return sb.ToString();
        }


        /// <summary>
        ///     ����debug��Ϣ
        /// </summary>
        /// <param name="debugInfo"></param>
        public void SetDebugInfo(string debugInfo)
        {
            DebugInfo = debugInfo;
        }

        public Hashtable GetAllParameters()
        {
            return Parameters;
        }

        protected virtual string GetCharset()
        {
            return HttpContext.Request.ContentEncoding.BodyName;
        }
    }
}