using System;

namespace Bots.Twitter
{
    public class MessageEventArgs : BaseEventArgs
    {
        public Message Message { get; set; }
    }
}
