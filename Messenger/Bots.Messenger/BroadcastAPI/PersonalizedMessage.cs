using Newtonsoft.Json;

namespace Messenger.Bot.BroadcastAPI
{
    public class PersonalizedMessage
    {
        public const string FirstName = "{{first_name}}";

        public const string LastName = "{{last_name}}";

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("fallback_text")]
        public string FallbackText { get; set; }
    }
}
