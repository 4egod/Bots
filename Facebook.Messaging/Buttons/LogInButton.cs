using Newtonsoft.Json;

namespace Facebook.Messaging.Buttons
{
    public class LogInButton : IButton
    {
        public ButtonTypes ButtonType => ButtonTypes.UrlButton;

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
