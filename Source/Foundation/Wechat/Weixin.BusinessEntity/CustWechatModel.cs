using Newtonsoft.Json;

namespace Smartac.Domain
{
    /// <summary>
    ///     会员扩展表-WeChat
    /// </summary>
    public class CustWechatModel
    {
        [JsonProperty("msgtype")]
        public string MsgType { get; set; }

        [JsonProperty("accountid")]
        public string AccountId { get; set; }

        [JsonProperty("openid")]
        public string OpenId { get; set; }

        [JsonProperty("tenantid")]
        public string TenantId { get; set; }

        [JsonProperty("opertime")]
        public string OperTime { get; set; }

        [JsonProperty("originalid")]
        public string OriginalId { get; set; }

        [JsonProperty("nickname")]
        public string NickName { get; set; }

        [JsonProperty("remarkname")]
        public string RemarkName { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("province")]
        public string Province { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("sex")]
        public string Sex { get; set; }

        [JsonProperty("headportait")]
        public string HeadPortait { get; set; }
    }
}