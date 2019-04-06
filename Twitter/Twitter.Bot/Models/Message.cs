using System;
using System.Collections.Generic;
using System.Text;

namespace Bots.Twitter
{
    public class Message
    {
        public long Id { get; set; }

        public DateTime Timestamp { get; set; }

        public long Sender { get; set; }

        public long Recipient { get; set; }

        public string Text { get; set; }

        public QuickReply QuickReply { get; set; }

        public QuickReplyResponse QuickReplyResponse { get; set; }
    }
}
