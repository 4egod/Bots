using System;

namespace Messenger
{
    public class MessageEventArgs : EventArgs
    {
        public string Sender { get; set; }

        public IncomeMessage Message { get; set; }
    }
}
