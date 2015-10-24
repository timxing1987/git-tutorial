using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Cedar.Framework.Common.BaseClasses
{
    /// <summary>
    /// </summary>
    public class JuheUtility
    {
        private readonly string _appkey = "1588412283f8faa413e128e832377e77";
        private readonly string _url = "http://op.juhe.cn/che300/query";

        /// <summary>
        ///     初始化
        /// </summary>
        public JuheUtility()
        {
            if (ConfigurationManager.AppSettings["juhe_appkey"] != null)
            {
                _appkey = ConfigurationManager.AppSettings["juhe_appkey"];
            }
            if (ConfigurationManager.AppSettings["juhe_url"] != null)
            {
                _url = ConfigurationManager.AppSettings["juhe_url"];
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public string GetUsedCarPrice(Dictionary<string, string> paramList)
        {
            var paramBase = new Dictionary<string, string>
            {
                {"key", _appkey},
                {"dtype", "json"},
                {"method", "getUsedCarPrice"}
            };

            //合并参数
            var parameters = paramBase.Concat(paramList).ToDictionary(k => k.Key, v => v.Value);

            var result = DynamicWebService.SendPost(_url, parameters, "get");

            var obj = JObject.Parse(result);
            var errorCode = obj["reason"].ToString();
            return errorCode != "success" ? "0" : obj["result"].ToString();
        }
    }
}