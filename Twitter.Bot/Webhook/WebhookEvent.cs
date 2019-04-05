using Newtonsoft.Json;
using System.Collections.Generic;
using Twitter.Bot.Models;

namespace Twitter.Bot.Webhook
{
    internal class WebhookEvent
    {
        [JsonProperty("for_user_id")]
        public long Recipient { get; set; }

        [JsonProperty("direct_message_events")]
        public List<MessageCreateEvent> DirectMessageEvents { get; set; }
    }
}
