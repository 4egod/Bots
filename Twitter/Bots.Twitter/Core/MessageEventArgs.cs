using System;

namespace Bots.Twitter
{
    public class MessageEventArgs : EventArgs
    {
        public Message Message { get; set; }
    }
}
