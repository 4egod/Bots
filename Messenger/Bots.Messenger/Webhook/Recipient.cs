using Newtonsoft.Json;

namespace Messenger.Bot.Webhook
{
    internal struct Recipient
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
