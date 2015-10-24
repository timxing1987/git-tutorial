/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
 
    �ļ�����RequestHandler.cs
    �ļ�����������΢��֧��V3 ������
    
    
    ������ʶ��Senparc - 20150211
    
    �޸ı�ʶ��Senparc - 20150303
    �޸������������ӿ�
----------------------------------------------------------------*/

using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.TenPayLibV3
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
        ///     ����md5ժҪ,������:����������a-z����,������ֵ�Ĳ������μ�ǩ��
        /// </summary>
        /// <param name="key">������</param>
        /// <param name="value">����ֵ</param>
        /// key��valueͨ������������һ�����
        /// <returns></returns>
        public virtual string CreateMd5Sign(string key, string value)
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

            sb.Append(key + "=" + value);
            var sign = MD5UtilHelper.GetMD5(sb.ToString(), GetCharset()).ToUpper();

            return sign;
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