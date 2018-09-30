using System.Collections.Generic;

namespace Facebook.Webhook
{
    internal struct Entry
    {
        public string id;
        public long time;
        public List<MessageContainer> messaging;
    }
}
