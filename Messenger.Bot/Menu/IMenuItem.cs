using Newtonsoft.Json;

namespace Messenger.Menu
{
    public interface IMenuItem
    {
        [JsonProperty("type")]
        MenuItemTypes ItemType { get; }
    }
}
