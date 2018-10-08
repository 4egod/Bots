using Newtonsoft.Json;

namespace Messenger.Menu
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
