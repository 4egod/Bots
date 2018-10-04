using Newtonsoft.Json;
using System.Collections.Generic;

namespace Messenger.SendAPI
{
    internal class Message
    {
        public string text;
        //public attachment;
        public List<QuickReply> quick_replies;
        public string metadata;
    }

    internal class Message<T> : Message
    {
        [JsonProperty("attachment")]
        public T Attachment { get; set; }
    }
}
