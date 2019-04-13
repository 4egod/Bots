using System.Collections.Generic;

namespace Bots.Twitter.Webhook
{
    using Models;
    using Newtonsoft.Json;

    internal class MessageEvent
    {
        [JsonProperty("direct_message_events")]
        public List<MessageCreateEvent> MessageEvents { get; set; }
    }
}
