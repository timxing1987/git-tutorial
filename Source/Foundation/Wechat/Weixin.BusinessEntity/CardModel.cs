using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cedar.Foundation.WeChat.Entities.WeChat
{
    /// <summary>
    ///     卡券
    /// </summary>
    public class CardModel
    {
        /// <summary>
        ///     微信帐号
        /// </summary>
        [JsonProperty("account")]
        public string Account { get; set; }

        /// <summary>
        ///     卡券的code 编码
        /// </summary>
        [JsonProperty("code")]
        public string Code { set; get; }

        /// <summary>
        ///     卡券ID
        /// </summary>
        [JsonProperty("cardid")]
        public string CardId { set; get; }

        /// <summary>
        ///     通过choose_card_info 获取的加密字符串
        /// </summary>
        [JsonProperty("encryptcode")]
        public string EncryptCode { set; get; }

        /// <summary>
        ///     查询卡列表的起始偏移量，从0 开始，即offset: 5 是指从从列表里的第六个开始读取。
        /// </summary>
        [JsonProperty("offset")]
        public string Offset { set; get; }

        /// <summary>
        ///     需要查询的卡片的数量（数量最大50）
        /// </summary>
        [JsonProperty("count")]
        public string Count { set; get; }


        /// <summary>
        ///     需要查询的卡片的数量（数量最大50）
        /// </summary>
        [JsonProperty("openids")]
        public List<string> Openids { set; get; }

        /// <summary>
        ///     需要查询的卡片的数量（数量最大50）
        /// </summary>
        [JsonProperty("usernames")]
        public List<string> UserNames { set; get; }
    }
}