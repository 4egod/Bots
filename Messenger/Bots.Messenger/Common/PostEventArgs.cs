using Microsoft.AspNetCore.Http;
using System;

namespace Messenger.Bot
{
    public class PostEventArgs : EventArgs
    {
        public IHeaderDictionary Headers { get; set; }

        public string Body { get; set; }
    }
}
