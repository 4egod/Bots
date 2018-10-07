using System.Collections.Generic;

namespace Messenger.BroadcastAPI
{
    using Newtonsoft.Json;
    using SendAPI;

    internal class BroadcastMessageContainer
    {
        [JsonProperty("messages")]
        public List<BroadcastMessage> Messages { get; set; }
    }
}
