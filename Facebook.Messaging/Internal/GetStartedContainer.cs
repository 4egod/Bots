using Newtonsoft.Json;

namespace Facebook.Messaging
{
    internal class GetStartedContainer
    {
        public class GetStartedValue
        {
            [JsonProperty("payload")]
            public string Payload { get; set; }
        }

        [JsonProperty("get_started")]
        public GetStartedValue Value { get; set; }
    }
}
