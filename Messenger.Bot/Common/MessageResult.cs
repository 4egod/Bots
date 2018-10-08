using Newtonsoft.Json;

namespace Messenger.Bot
{
    public class MessageResult : BaseResult
    {
        /// <summary>
        /// Message Id.
        /// </summary>
        [JsonProperty("message_id")]
        public override string Id { get; set; }

        [JsonProperty("recipient_id")]
        public string RecepientId { get; set; }
    }
}
