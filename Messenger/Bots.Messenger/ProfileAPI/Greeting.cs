using Newtonsoft.Json;
using System.Collections.Generic;

namespace Messenger.Bot.ProfileAPI
{
    internal class Greeting
    {
        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    internal class GreetingContainer
    {
        [JsonProperty("greeting")]
        public List<Greeting> Values { get; set; }
    }
}
