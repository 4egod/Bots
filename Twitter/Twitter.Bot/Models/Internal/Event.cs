using System;
using Newtonsoft.Json;

namespace Bots.Twitter.Models
{
    internal abstract class Event : IEvent
    {
        //[JsonProperty("type")]
        public abstract string Type { get; }

        //[JsonProperty("id")]
        public long Id { get; set; }

        //[JsonProperty("created_timestamp")]
        public DateTime Timestamp { get; set; }
    }

    internal class Event<T> where T : IEvent
    {
        [JsonProperty("event")]
        public T Data { get; set; }
    }
}
