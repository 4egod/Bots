using Newtonsoft.Json;

namespace Messenger.Buttons
{
    public class LogInButton : IButton
    {
        public ButtonTypes ButtonType => ButtonTypes.UrlButton;

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
