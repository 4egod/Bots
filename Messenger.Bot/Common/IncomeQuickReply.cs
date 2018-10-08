using Newtonsoft.Json;

namespace Messenger.Bot
{
    public class IncomeQuickReply
    {
        [JsonProperty("payload")]
        public string Payload { get; set; }
    }
}
