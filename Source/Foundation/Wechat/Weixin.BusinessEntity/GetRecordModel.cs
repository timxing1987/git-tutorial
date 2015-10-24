using System;
using Newtonsoft.Json;

namespace Cedar.Foundation.WeChat.Entities.WeChat
{
    public class GetRecordModel
    {
        /// <summary>
        /// </summary>
        [JsonProperty("account")]
        public string Account { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("starttime")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("endtime")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("pagesize")]
        public int PageSize { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("pageindex")]
        public int PageIndex { get; set; }
    }
}