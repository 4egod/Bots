using Newtonsoft.Json;

namespace Messenger.Buttons
{
    public interface IButton
    {
        [JsonProperty("type")]
        ButtonTypes ButtonType { get; }
    }
}
