using Newtonsoft.Json;

namespace Messenger.ProfileAPI
{
    internal class GetStarted
    {
        [JsonProperty("payload")]
        public string Payload { get; set; }
    }

    internal class GetStartedContainer
    {
        [JsonProperty("get_started")]
        public GetStarted Value { get; set; }
    }
}
