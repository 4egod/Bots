using Newtonsoft.Json;

namespace Twitter.Bot.Models
{
    public class MessageCreateEvent : Event
    {
        public override string Type => "message_create";

        [JsonProperty("message_create")]
        public MessageCreateData Data { get; set; }
    }
}
