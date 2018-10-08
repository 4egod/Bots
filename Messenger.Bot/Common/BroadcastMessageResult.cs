using Newtonsoft.Json;

namespace Messenger.Bot
{
    public class BroadcastMessageResult : BaseResult
    {
        [JsonProperty("message_creative_id")]
        public override string Id { get; set; }
    }
}
