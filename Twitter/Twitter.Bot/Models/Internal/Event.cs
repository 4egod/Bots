using System;
using Newtonsoft.Json;

namespace Twitter.Bot.Models
{
    public abstract class Event : IEvent
    {
        //[JsonProperty("type")]
        public abstract string Type { get; }

        //[JsonProperty("id")]
        public long Id { get; set; }

        //[JsonProperty("created_timestamp")]
        public DateTime Timestamp { get; set; }
    }

    public class Event<T> where T : IEvent
    {
        [JsonProperty("event")]
        public T Data { get; set; }
    }
}
