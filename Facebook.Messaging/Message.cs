using System.Collections.Generic;

namespace Facebook.Messaging
{
    internal struct Message
    {
        public string text;
        //public attachment;
        public List<QuickReply> quick_replies;
        public string metadata;
    }
}
