using System;
using Newtonsoft.Json;

namespace Cedar.Foundation.WeChat.Entities.WeChat
{
    public class MesgTemplateModel
    {
        #region Model

        public string Shopid { get; set; }

        /// <summary>
        /// </summary>
        public string Accountid { get; set; }

        /// <summary>
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("template_id_short")]
        public string TemplateIdShort { get; set; }

        /// <summary>
        /// </summary>
        public string Tital { get; set; }

        /// <summary>
        /// </summary>
        public int? Channel { get; set; }

        /// <summary>
        /// </summary>
        public int? State { get; set; }

        /// <summary>
        /// </summary>
        public int Templatetype { get; set; }

        /// <summary>
        /// </summary>
        public string Sample { get; set; }

        /// <summary>
        /// </summary>
        public string Templateid { get; set; }

        /// <summary>
        /// </summary>
        public DateTime? Createtime { get; set; }

        #endregion Model
    }

    public class TemplateNew
    {
        [JsonProperty("InnerId")]
        public string Innerid { get; set; }

        [JsonProperty("typeid")]
        public int Typeid { get; set; }

        [JsonProperty("typename")]
        public string Typename { get; set; }

        [JsonProperty("issms")]
        public string Issms { get; set; }

        [JsonProperty("isemail")]
        public string Isemail { get; set; }

        [JsonProperty("iswechat")]
        public string Iswechat { get; set; }

        [JsonProperty("smsinfo")]
        public string Smsinfo { get; set; }

        [JsonProperty("emailinfo")]
        public string Emailinfo { get; set; }

        [JsonProperty("wechatinfo")]
        public string Wechatinfo { get; set; }

        [JsonProperty("tempid")]
        public string Tempid { get; set; }

        [JsonProperty("createdtime")]
        public string Createdtime { get; set; }

        public string TemplateIdShort { get; set; }
    }
}