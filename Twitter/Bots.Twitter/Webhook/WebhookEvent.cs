using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bots.Twitter.Webhook
{
    using Models;

    internal class WebhookEvent
    {
        [JsonProperty("for_user_id")]
        public long Recipient { get; set; }

        [JsonProperty("direct_message_events")]
        public List<MessageCreateEvent> DirectMessageEvents { get; set; }

        [JsonProperty("follow_events")]
        public List<FollowEvent> FollowEvents { get; set; }
    }
}
