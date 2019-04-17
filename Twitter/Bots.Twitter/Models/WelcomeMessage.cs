using System;

namespace Bots.Twitter
{
    public class WelcomeMessage
    {
        public long Id { get; set; }

        public DateTime Timestamp { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public QuickReply QuickReply { get; set; }
    }
}
