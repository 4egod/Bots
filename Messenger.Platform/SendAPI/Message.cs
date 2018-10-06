using Newtonsoft.Json;
using System.Collections.Generic;

namespace Messenger.SendAPI
{
    internal class Message
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("quick_replies")]
        public List<QuickReply> QuickReplies;

        [JsonProperty("metadata")]
        public string Metadata;
    }

    internal class Message<T> : Message where T : IAttachment
    {
        [JsonProperty("attachment")]
        public Attachment<T> Attachment { get; set; }
    }
}
