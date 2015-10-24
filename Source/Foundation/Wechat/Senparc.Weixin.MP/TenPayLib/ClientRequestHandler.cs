using System.Collections;
using System.Text;
using System.Web;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.TenPayLib
{
    public class ClientRequestHandler
    {
        /// <summary>
        ///     debug信息
        /// </summary>
        private string DebugInfo;

        /// <summary>
        ///     网关url地址
        /// </summary>
        private string GateUrl;

        protected HttpContext HttpContext;

        /// <summary>
        ///     密钥
        /// </summary>
        private string Key;

        /// <summary>
        ///     请求的参数
        /// </summary>
        protected Hashtable Parameters;

        public ClientRequestHandler(HttpContext httpContext)
        {
            Parameters = new Hashtable();

            HttpContext = httpContext ?? HttpContext.Current;
        }

        /// <summary>
        ///     初始化函数
        /// </summary>
        public virtual void Init()
        {
            //nothing to do
        }

        /// <summary>
        ///     获取入口地址,不包含参数值
        /// </summary>
        /// <returns></returns>
        public string GetGateUrl()
        {
            return GateUrl;
        }

        /// <summary>
        ///     设置入口地址,不包含参数值
        /// </summary>
        /// <param name="gateUrl"></param>
        public void SetGateUrl(string gateUrl)
        {
            GateUrl = gateUrl;
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
        ///     获取带参数的请求URL  @return String
        /// </summary>
        /// <returns></returns>
        public virtual string GetRequestURL()
        {
            CreateSign();

            var sb = new StringBuilder();
            var akeys = new ArrayList(Parameters.Keys);
            akeys.Sort();
            foreach (string k in akeys)
            {
                var v = (string) Parameters[k];
                if (null != v && "key".CompareTo(k) != 0 && "spbill_create_ip".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + TenPayUtil.UrlEncode(v, GetCharset()) + "&");
                }
                else if ("spbill_create_ip".CompareTo(k) == 0)
                {
                    sb.Append(k + "=" + v.Replace(".", "%2E") + "&");
                }
            }

            //去掉最后一个&
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return GetGateUrl() + "?" + sb;
        }

        /// <summary>
        ///     创建md5摘要,规则是:按参数名称a-z排序,遇到空值的参数不参加签名。
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
            var sign = MD5UtilHelper.GetMD5(sb.ToString(), GetCharset()).ToLower();

            SetParameter("sign", sign);

            //debug信息
            SetDebugInfo(sb + " &sign=" + sign);
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

        public void DoSend()
        {
            HttpContext.Response.Redirect(GetRequestURL());
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