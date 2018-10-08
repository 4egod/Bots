using System;

namespace Messenger.Bot
{
    public class MessageEventArgs : EventArgs
    {
        public string Sender { get; set; }

        public IncomeMessage Message { get; set; }
    }
}
