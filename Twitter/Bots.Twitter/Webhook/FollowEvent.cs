using Newtonsoft.Json;
using System;

namespace Bots.Twitter.Webhook
{
    using Converters;

    internal class FollowEvent 
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("created_timestamp")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        [JsonProperty("target")]
        public User Target { get; set; }

        [JsonProperty("source")]
        public User Source { get; set; }
    }
}
