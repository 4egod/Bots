using Newtonsoft.Json;

namespace Messenger
{
    public class IncomeQuickReply
    {
        [JsonProperty("payload")]
        public string Payload { get; set; }
    }
}
