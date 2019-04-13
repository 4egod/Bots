using Newtonsoft.Json;

namespace Messenger.Bot.Buttons
{
    public interface IButton
    {
        [JsonProperty("type")]
        ButtonTypes ButtonType { get; }
    }
}
