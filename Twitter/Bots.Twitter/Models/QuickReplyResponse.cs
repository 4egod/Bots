using Newtonsoft.Json;

namespace Bots.Twitter
{
    public class QuickReplyResponse
    {
        [JsonProperty("type")]
        internal string Type { get; set; }

        [JsonProperty("metadata")]
        public string Metadata { get; set; }
    }
}
