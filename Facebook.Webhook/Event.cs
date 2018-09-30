using System.Collections.Generic;

namespace Facebook.Webhook
{
    internal struct Event
    {
        public string @object;
        public List<Entry> entry;
    }
}
