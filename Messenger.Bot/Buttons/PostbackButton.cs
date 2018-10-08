using Newtonsoft.Json;

namespace Messenger.Buttons
{
    public class PostbackButton : IButton
    {
        public ButtonTypes ButtonType => ButtonTypes.PostbackButton;

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("payload")]
        public string Payload { get; set; }
    }
}
