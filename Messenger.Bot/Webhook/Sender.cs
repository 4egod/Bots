using Newtonsoft.Json;

namespace Messenger.Webhook
{
    internal struct Sender
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
