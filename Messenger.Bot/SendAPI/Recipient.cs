using Newtonsoft.Json;

namespace Messenger.Bot.SendAPI
{
    internal struct Recipient
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber;

        [JsonProperty("user_ref")]
        public string UserRef;
    }
}
