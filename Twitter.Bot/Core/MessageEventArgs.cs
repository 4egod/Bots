using System;

namespace Twitter.Bot
{
    public class MessageEventArgs : EventArgs
    {
        public Message Message { get; set; }
    }
}
