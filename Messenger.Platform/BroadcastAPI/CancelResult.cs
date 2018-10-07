using Newtonsoft.Json;

namespace Messenger.BroadcastAPI
{
    internal class CancelResult
    {
        [JsonProperty("success")]
        public bool Result { get; set; }
    }
}
