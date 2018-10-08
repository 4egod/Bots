using Newtonsoft.Json;

namespace Messenger.ProfileAPI
{
    internal class OperationResult
    {
        [JsonProperty("result")]
        public string Value { get; set; }

        public bool Result => Value == "success";
    }
}
