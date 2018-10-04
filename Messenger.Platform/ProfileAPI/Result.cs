using Newtonsoft.Json;

namespace Messenger.ProfileAPI
{
    internal class Result
    {
        [JsonProperty("result")]
        public string Value { get; set; }

        public bool IsOk => Value == "success";
    }
}
