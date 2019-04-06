using Newtonsoft.Json;

namespace Bots.Twitter.Models
{
    internal class Target
    {
        [JsonProperty("recipient_id")]
        public long RecipientId { get; set; }
    }
}
