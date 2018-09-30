using Newtonsoft.Json;

namespace Facebook.Messaging.Buttons
{
    public interface IButton
    {
        [JsonProperty("type")]
        ButtonTypes ButtonType { get; }
    }
}
