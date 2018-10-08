using Newtonsoft.Json;

namespace Messenger
{
    public class QuickReply
    {
        [JsonProperty("content_type")]
        public QuickReplyTypes QuickReplyType { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("payload")]
        public string Payload { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
    }
}
