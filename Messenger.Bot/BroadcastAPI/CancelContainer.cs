using Newtonsoft.Json;

namespace Messenger.BroadcastAPI
{
    internal class CancelContainer
    {
        [JsonProperty("operation")]
        public string Operation => "cancel";
    }
}
