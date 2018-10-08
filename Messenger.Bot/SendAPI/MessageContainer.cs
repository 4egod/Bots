using Newtonsoft.Json;

namespace Messenger.Bot.SendAPI
{
    internal struct MessageContainer
    {
        [JsonProperty("recipient")]
        public Recipient Recipient { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }
    } 
}
