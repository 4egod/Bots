using System.Collections.Generic;

namespace Messenger.Webhook
{
    internal struct Entry
    {
        public string id;
        public long time;
        public List<MessageContainer> messaging;
    }
}
