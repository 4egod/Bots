using System;
using System.Collections.Generic;
using System.Text;

namespace Bots.Twitter
{
    public abstract class BaseEventArgs : EventArgs
    {
        public long Recipient { get; set; }
    }
}
