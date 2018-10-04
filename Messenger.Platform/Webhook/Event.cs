using System.Collections.Generic;

namespace Messenger.Webhook
{
    internal struct Event
    {
        public string @object;
        public List<Entry> entry;
    }
}
