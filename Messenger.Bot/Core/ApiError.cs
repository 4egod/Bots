using Newtonsoft.Json;

namespace Messenger.Bot
{
    public class ApiError
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("fbtrace_id")]
        public string TraceId { get; set; }
    }

    public class ApiErrorContainer
    {
        [JsonProperty("error")]
        public ApiError Error { get; set; }
    }
}
