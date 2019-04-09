using System;

namespace Bots.Twitter
{
    public class DirectMessageEventArgs : EventArgs
    {
        public Message Message { get; set; }
    }
}
