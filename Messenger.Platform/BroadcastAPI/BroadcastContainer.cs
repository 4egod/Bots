using Newtonsoft.Json;

namespace Messenger.BroadcastAPI
{
    internal class BroadcastContainer
    {
        [JsonProperty("message_creative_id")]
        public string CreativeMessageId { get; set; }

        [JsonProperty("notification_type")]
        public NotificationTypes NotificationType { get; set; }

        [JsonProperty("messaging_type")]
        public MessagingTypes MessagingType => MessagingTypes.MessageTag;

        [JsonProperty("tag")]
        public string Tag => "NON_PROMOTIONAL_SUBSCRIPTION";

        [JsonProperty("schedule_time")]
        public string ScheduleTime { get; set; }
    }
}
