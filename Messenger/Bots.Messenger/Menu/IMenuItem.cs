using Newtonsoft.Json;

namespace Messenger.Bot.Menu
{
    public interface IMenuItem
    {
        [JsonProperty("type")]
        MenuItemTypes ItemType { get; }
    }
}
