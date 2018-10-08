using Newtonsoft.Json;
using System.Collections.Generic;

namespace Messenger.Webhook
{
    internal struct Entry
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("messaging")]
        public List<GenericContainer> Items { get; set; }
    }
}
