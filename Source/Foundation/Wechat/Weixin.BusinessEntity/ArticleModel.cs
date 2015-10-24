using Newtonsoft.Json;

namespace Cedar.Foundation.WeChat.Entities.WeChat
{
    public class ArticleModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("picUrl")]
        public string PicUrl { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("digest")]
        public string Digest { get; set; }
    }
}