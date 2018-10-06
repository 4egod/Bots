using Newtonsoft.Json;

namespace Messenger
{
    public struct MessageResult
    {
        [JsonProperty("recipient_id")]
        public string RecepientId { get; set; }

        [JsonProperty("message_id")]
        public string MessageId { get; set; }

        [JsonIgnore]
        public bool Result => !string.IsNullOrEmpty(MessageId);

        public static MessageResult Failed => new MessageResult();
    }
}
