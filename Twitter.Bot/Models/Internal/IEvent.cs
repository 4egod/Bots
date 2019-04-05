using System;
using Newtonsoft.Json;

namespace Twitter.Bot.Models
{
    using Converters;

    public interface IEvent
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
