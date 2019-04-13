using System;

namespace Messenger.Bot
{
    public class PostbackEventArgs : EventArgs
    {
        public string Sender { get; set; }

        public Postback Postback { get; set; }
    }
}
