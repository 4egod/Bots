using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bots.Twitter
{
    using Converters;

    /// <summary>
    /// TODO: Unfinished
    /// </summary>
    public class Tweet
    {
        [JsonProperty("created_at")]
        //public DateTime CreatedAt { get; set; }
        public string CreatedAt { get; set; }



        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("id_str")]
        public string IdAsString { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("in_reply_to_status_id")]
        public long? ReplyToStatusId { get; set; }

        [JsonProperty("in_reply_to_status_id_str")]
        public string ReplyToStatusIdAsString { get; set; }

        [JsonProperty("in_reply_to_user_id")]
        public long? ReplyToUserId { get; set; }

        [JsonProperty("in_reply_to_user_id_str")]
        public string ReplyToUserIdAsString { get; set; }

        [JsonProperty("in_reply_to_screen_name")]
        public string ReplyToUser { get; set; }

        [JsonProperty("user")]
        public User Creator { get; set; }

        [JsonProperty("retweeted_status")]
        public Tweet RetweetedFrom { get; set; }

        [JsonProperty("quoted_status")]
        public Tweet QuotedFrom { get; set; }


        [JsonProperty("retweet_count")]
        public int RetweetCount { get; set; }

        [JsonProperty("timestamp_ms")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
