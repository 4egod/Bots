using Microsoft.AspNetCore.Http;
using System;

namespace Bot
{
    public class PostEventArgs : EventArgs
    {
        public IHeaderDictionary Headers { get; set; }

        public string Body { get; set; }
    }
}
