using Newtonsoft.Json;

namespace Messenger.Buttons
{
    public class CallButton : IButton
    {
        public ButtonTypes ButtonType => ButtonTypes.CallButton;

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("payload")]
        public string Payload { get; set; }
    }
}
