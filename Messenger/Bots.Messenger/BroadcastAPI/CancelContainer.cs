using Newtonsoft.Json;

namespace Messenger.Bot.BroadcastAPI
{
    internal class CancelContainer
    {
        [JsonProperty("operation")]
        public string Operation => "cancel";
    }
}
