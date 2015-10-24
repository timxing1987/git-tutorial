using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cedar.Foundation.WeChat.Entities.WeChat
{
    public class MesgModel
    {
        /// <summary>
        /// </summary>
        [JsonProperty("media_id")] public string MediaId;

        /// <summary>
        /// </summary>
        [JsonProperty("thumbmediaid")] public string ThumbMediaId;

        /// <summary>
        /// </summary>
        [JsonProperty("openid")]
        public List<string> OpenId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("hqmusicurl")]
        public string HqMusicUrl { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("account")]
        public string Account { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("news")]
        public dynamic News { get; set; }

        /// <summary>
        ///     微信code
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("article")]
        public List<ArticleModel> Article { get; set; }

        [JsonProperty("mediatype")]
        public string MediaType { get; set; }

        [JsonProperty("paramex")]
        public string ParamEx { get; set; }
    }

    public class GroupMesgModel
    {
        /// <summary>
        /// </summary>
        [JsonProperty("account")]
        public string Account { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("openid")]
        public List<string> OpenId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("groupid")]
        public string GroupId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("jsoncontent")]
        public string JsonContent { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("securityid")]
        public string SecurityId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("paramex")]
        public string ParamEx { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("media_id")]
        public string Mediaid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("news")]
        public dynamic News { get; set; }
    }

    public class SendTempletModel
    {
        /// <summary>
        /// </summary>
        [JsonProperty("account")]
        public string Account { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("openid")]
        public List<string> OpenId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("Shopid")]
        public string Shopid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("templateidshort")]
        public string TemplateIdShort { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("topcolor")]
        public string Topcolor { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("data")]
        public dynamic Data { get; set; }
    }

    public class QrCodemodel
    {
        /// <summary>
        /// </summary>
        [JsonProperty("account")]
        public string Account { get; set; }

        /// <summary>
        ///     ///
        ///     <param name="expireSeconds">该二维码有效时间，以秒为单位。 最大不超过1800。0时为永久二维码</param>
        /// </summary>
        [JsonProperty("expireseconds")]
        public string ExpireSeconds { get; set; }

        /// <summary>
        ///     ///
        ///     <param name="sceneId">场景值ID，临时二维码时为32位整型，永久二维码时最大值为1000</param>
        /// </summary>
        [JsonProperty("sceneid")]
        public string SceneId { get; set; }
    }

    public class MesgGetuser
    {
        /// <summary>
        ///     帐号
        /// </summary>
        [JsonProperty("account")]
        public string Account { get; set; }

        /// <summary>
        ///     下一个openid
        /// </summary>
        [JsonProperty("nextopenid")]
        public string NextOpenId { get; set; }
    }

    public class AnalysisModel
    {
        /// <summary>
        ///     帐号
        /// </summary>
        [JsonProperty("account")]
        public string Account { get; set; }

        /// <summary>
        ///     开始时间
        /// </summary>
        [JsonProperty("begindate")]
        public string BeginDate { get; set; }

        /// <summary>
        ///     结束时间
        /// </summary>
        [JsonProperty("enddate")]
        public string EndDate { get; set; }
    }

    public class ParamEx
    {
        [JsonProperty("activityid")]
        public string ActivityId { get; set; }
    }

    public class AccesstokenModel
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("appid")]
        public string Appid { get; set; }
    }
}