using Newtonsoft.Json;

namespace Messenger.BroadcastAPI
{
    using SendAPI;

    internal class BroadcastMessage : Message
    {
        [JsonIgnore]
        public override string Metadata { get; set; }
    }
}
