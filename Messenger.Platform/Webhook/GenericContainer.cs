using Newtonsoft.Json;

namespace Messenger.Webhook
{
    internal class GenericContainer
    {
        [JsonProperty("sender")]
        public Sender Sender { get; set; }

        [JsonProperty("recipient")]
        public Recipient Recipient { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("message")]
        public IncomeMessage Message { get; set; }

        [JsonProperty("postback")]
        public Postback Postback { get; set; }
    }
}
