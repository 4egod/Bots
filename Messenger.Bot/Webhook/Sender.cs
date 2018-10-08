using Newtonsoft.Json;

namespace Messenger.Bot.Webhook
{
    internal struct Sender
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
