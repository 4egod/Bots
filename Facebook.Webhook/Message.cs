using System;
using System.Collections.Generic;
using System.Text;

namespace Facebook.Webhook
{
    internal class Message
    {
        public string mid;
        public long seq;
        public string text;
        public List<Attachment> attachments;
        public QuickReply quick_reply;
    }
}
