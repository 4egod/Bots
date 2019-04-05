using System.Collections.Generic;

namespace Twitter.Bot.Webhook
{
    using Models;
    using Newtonsoft.Json;

    internal class DirectMessageEvent
    {
        [JsonProperty("direct_message_events")]
        public List<MessageCreateEvent> DirectMessageEvents { get; set; }
    }
}
