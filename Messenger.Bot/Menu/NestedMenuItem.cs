using Newtonsoft.Json;
using System.Collections.Generic;

namespace Messenger.Bot.Menu
{
    public class NestedMenuItem : IMenuItem
    {
        public MenuItemTypes ItemType => MenuItemTypes.NestedMenuItem;

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("call_to_actions")]
        public List<IMenuItem> Items { get; set; }
    }
}
