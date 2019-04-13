using Newtonsoft.Json;

namespace Messenger.Bot
{
    public class Postback
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("payload")]
        public string Payload { get; set; }
    }
}
