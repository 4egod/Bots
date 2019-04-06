using Microsoft.AspNetCore.Http;
using System;

namespace Bot
{
    public class WebhookEventArgs : EventArgs
    {
        public HttpRequest Request { get; set; }

        public HttpResponse Response { get; set; }
    }
}
