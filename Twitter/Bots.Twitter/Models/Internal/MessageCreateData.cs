using Newtonsoft.Json;

namespace Bots.Twitter.Models
{
    internal class MessageCreateData
    {
        [JsonProperty("target")]
        public Target Target { get; set; }

        [JsonProperty("message_data")]
        public MessageData Data { get; set; }

        [JsonProperty("sender_id")]
        public long Sender { get; set; }
    }
}
