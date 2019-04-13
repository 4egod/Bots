using Newtonsoft.Json;
using System;

namespace Bots.Twitter.Webhook
{
    using Converters;

    internal class LikeEvent
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("timestamp_ms")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        [JsonProperty("favorited_status")]
        public Tweet Tweet { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
