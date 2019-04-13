using Newtonsoft.Json;

namespace Messenger.Bot
{
    public class IncomeMessage
    {
        [JsonProperty("mid")]
        public string MessageId { get; set; }

        [JsonProperty("seq")]
        public int Sequence { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("quick_reply")]
        public IncomeQuickReply QuickReply { get; set; }
    }
}
