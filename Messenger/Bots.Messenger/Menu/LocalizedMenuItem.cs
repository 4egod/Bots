using Newtonsoft.Json;
using System.Collections.Generic;

namespace Messenger.Bot.Menu
{
    public class LocalizedMenuItem
    {
        public const string DefaultLocale = "default";

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("composer_input_disabled")]
        public bool ComposerInputDisabled { get; set; }

        [JsonProperty("call_to_actions")]
        public List<IMenuItem> Items { get; set; }
    }
}
