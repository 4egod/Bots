using Newtonsoft.Json;
using System.Collections.Generic;

namespace Messenger.Webhook
{
    internal struct Event
    {
        [JsonProperty("object")]
        public string @Object { get; set; }

        [JsonProperty("entry")]
        public List<Entry> Entries { get; set; }
    }
}
