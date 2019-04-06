using System;
using Newtonsoft.Json;

namespace Bots.Twitter.Models
{
    using Converters;

    internal interface IEvent
    {
        [JsonProperty("type")]
        string Type { get; }

        [JsonProperty("id")]
        long Id { get; set; }

        [JsonProperty("created_timestamp")]
        [JsonConverter(typeof(DateTimeConverter))]
        DateTime Timestamp { get; set; }
    }
}
