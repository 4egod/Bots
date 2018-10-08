using Newtonsoft.Json;

namespace Messenger
{
    public class BroadcastResult : BaseResult
    {
        [JsonProperty("broadcast_id")]
        public override string Id { get; set; }
    }
}
