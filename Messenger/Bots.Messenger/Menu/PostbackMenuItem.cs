using Newtonsoft.Json;

namespace Messenger.Bot.Menu
{
    public class PostbackMenuItem : IMenuItem
    {
        public MenuItemTypes ItemType => MenuItemTypes.PostbackMenuItem;

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("payload")]
        public string Payload { get; set; }
    }
}
