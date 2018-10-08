using Newtonsoft.Json;

namespace Messenger.Bot.Menu
{
    public class UrlMenuItem : IMenuItem
    {
        public MenuItemTypes ItemType => MenuItemTypes.UrlMenuItem;

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
