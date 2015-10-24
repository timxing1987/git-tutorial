using System.Collections;
using System.Text;
using System.Xml;
using Senparc.Weixin.MP.Helpers;

/**
 * 后台应答类
 * ============================================================================
 * api说明：
 * getKey()/setKey(),获取/设置密钥
 * getContent() / setContent(), 获取/设置原始内容
 * getParameter()/setParameter(),获取/设置参数值
 * getAllParameters(),获取所有参数
 * isTenpaySign(),是否财付通签名,true:是 false:否
 * getDebugInfo(),获取debug信息
 * 
 * ============================================================================
 *
 */

namespace Senparc.Weixin.MP.TenPayLib
{
    public class ClientResponseHandler
    {
        private string Charset = "gb2312";

        /// <summary>
        ///     原始内容
        /// </summary>
        protected string Content;

        /// <summary>
        ///     debug信息
        /// </summary>
        private string DebugInfo;

        /// <summary>
        ///     密钥
        /// </summary>
        private string Key;

        /// <summary>
        ///     应答的参数
        /// </summary>
        protected Hashtable Parameters;


        /// <summary>
        ///     获取服务器通知数据方式，进行参数获取
        /// </summary>
        public ClientResponseHandler()
        {
            Parameters = new Hashtable();
        }

        public string GetContent()
        {
            return Content;
        }

        public virtual void SetContent(string content)
        {
            Content = content;
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(content);
            var root = xmlDoc.SelectSingleNode("root");
            var xnl = root.ChildNodes;

            foreach (XmlNode xnf in xnl)
            {
                SetParameter(xnf.Name, xnf.InnerXml);
            }
        }

        /// <summary>
        ///     获取密钥
        /// </summary>
        /// <returns></returns>
        public string GetKey()
        {
            return Key;
        }

        /// <summary>
        ///     设置密钥
        /// </summary>
        /// <param name="key"></param>
        public void SetKey(string key)
        {
            Key = key;
        }

        /// <summary>
        ///     获取参数值
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public string GetParameter(string parameter)
        {
            var s = (string) Parameters[parameter];
            return (null == s) ? "" : s;
        }

        /// <summary>
        ///     设置参数值
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
        ///     是否财付通签名,规则是:按参数名称a-z排序,遇到空值的参数不参加签名 @return boolean
        /// </summary>
        /// <returns></returns>
        public virtual bool IsTenpaySign()
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
            var sign = MD5UtilHelper.GetMD5(sb.ToString(), getCharset()).ToLower();

            //debug信息
            SetDebugInfo(sb + " => sign:" + sign);
            return GetParameter("sign").ToLower().Equals(sign);
        }

        /// <summary>
        ///     获取debug信息
        /// </summary>
        /// <returns></returns>
        public string GetDebugInfo()
        {
            return DebugInfo;
        }

        /// <summary>
        ///     设置debug信息
        /// </summary>
        /// <param name="debugInfo"></param>
        protected void SetDebugInfo(string debugInfo)
        {
            DebugInfo = debugInfo;
        }

        protected virtual string getCharset()
        {
            return Charset;
        }

        public void SetCharset(string charset)
        {
            Charset = charset;
        }

        /// <summary>
        ///     是否财付通签名,规则是:按参数名称a-z排序,遇到空值的参数不参加签名  @return boolean
        /// </summary>
        /// <param name="aKeys"></param>
        /// <returns></returns>
        public virtual bool IsTenpaySign(ArrayList aKeys)
        {
            var sb = new StringBuilder();

            foreach (string k in aKeys)
            {
                var v = (string) Parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append("key=" + GetKey());
            var sign = MD5UtilHelper.GetMD5(sb.ToString(), getCharset()).ToLower();

            //debug信息
            SetDebugInfo(sb + " => sign:" + sign);
            return GetParameter("sign").ToLower().Equals(sign);
        }
    }
}