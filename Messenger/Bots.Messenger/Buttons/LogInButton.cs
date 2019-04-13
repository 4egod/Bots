using Newtonsoft.Json;

namespace Messenger.Bot.Buttons
{
    public class LogInButton : IButton
    {
        public ButtonTypes ButtonType => ButtonTypes.UrlButton;

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
