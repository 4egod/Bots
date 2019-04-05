using Newtonsoft.Json;

namespace Twitter.Bot.Models
{
    public class MessageData
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        
        [JsonProperty("quick_reply")]
        public QuickReply QuickReply { get; set; }

        [JsonProperty("quick_reply_response")]
        public QuickReplyResponse QuickReplyResponse { get; set; }
    }
}
