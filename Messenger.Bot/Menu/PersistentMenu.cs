using Newtonsoft.Json;
using System.Collections.Generic;

namespace Messenger.Menu
{
    public class PersistentMenu
    {
        [JsonProperty("persistent_menu")]
        public List<LocalizedMenuItem> Items { get; set; }
    }
}
