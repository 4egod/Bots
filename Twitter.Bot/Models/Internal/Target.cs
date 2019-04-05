using Newtonsoft.Json;

namespace Twitter.Bot.Models
{
    public class Target
    {
        [JsonProperty("recipient_id")]
        public long RecipientId { get; set; }
    }
}
