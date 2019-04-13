using Newtonsoft.Json;
using System.Collections.Generic;

namespace Messenger.Bot.SendAPI
{
    /// <summary>
    /// Simple text message
    /// </summary>
    internal class Message
    {
        /// <summary>
        /// Message text. Previews will not be shown for the URLs in this field. Use attachment instead. Must be UTF-8 and has a 2000 character limit. text or attachment must be set.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Optional. Array of quick_reply to be sent with messages
        /// </summary>
        [JsonProperty("quick_replies")]
        public List<QuickReply> QuickReplies { get; set; }

        /// <summary>
        /// Optional. Custom string that is delivered as a message echo. 1000 character limit.
        /// </summary>
        [JsonProperty("metadata")]
        public virtual string Metadata { get; set; }
    }

    /// <summary>
    /// Message with an attachment
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Message<T> : Message where T : IAttachment
    {
        [JsonProperty("attachment")]
        public Attachment<T> Attachment { get; set; }
    }
}
