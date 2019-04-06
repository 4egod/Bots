using Newtonsoft.Json;
using System.Collections.Generic;

namespace Twitter.Bot
{
    public class ApiError
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    internal class ApiErrorContainer
    {
        [JsonProperty("errors")]
        public List<ApiError> Errors { get; set; }
    }
}
