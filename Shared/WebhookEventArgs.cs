using Microsoft.AspNetCore.Http;
using System;

namespace Bots
{
    public class WebhookEventArgs : EventArgs
    {
        public HttpRequest Request { get; set; }

        public bool IsValid { get; set; }

        public byte[] BodyRaw { get; set; }

        public string Body { get; set; }

        public HttpResponse Response { get; set; }
    }
}
