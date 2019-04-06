using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bots.Twitter
{
    public class QuickReply
    {
        [JsonProperty("type")]
        internal string Type { get; set; } = "options";

        [JsonProperty("options")]
        public List<QuickReplyOption> Options { get; set; }
    }
}
