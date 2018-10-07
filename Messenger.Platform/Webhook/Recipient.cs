using Newtonsoft.Json;

namespace Messenger.Webhook
{
    internal struct Recipient
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
