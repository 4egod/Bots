using Newtonsoft.Json;

namespace Messenger.Bot.BroadcastAPI
{
    using SendAPI;

    internal class BroadcastMessage : Message
    {
        [JsonIgnore]
        public override string Metadata { get; set; }
    }
}
