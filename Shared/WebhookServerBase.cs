using System;
using System.Collections.Generic;
using System.Text;

namespace Bot
{
    public abstract class WebhookServerBase
    {
        public abstract string WebhookPath { get; }

        public abstract string StatusPath { get; }
    }
}
