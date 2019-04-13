using Newtonsoft.Json;

namespace Messenger.Bot.BroadcastAPI
{
    internal class CancelResult
    {
        [JsonProperty("success")]
        public bool Result { get; set; }
    }
}
