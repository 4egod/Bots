using System;

namespace Messenger.Webhook
{
    public class PostEventArgs : EventArgs
    {
        public string Body { get; set; }
    }
}
