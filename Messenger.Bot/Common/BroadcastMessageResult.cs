using Newtonsoft.Json;

namespace Messenger.BroadcastAPI
{
    public class BroadcastMessageResult : BaseResult
    {
        [JsonProperty("message_creative_id")]
        public override string Id { get; set; }
    }
}
