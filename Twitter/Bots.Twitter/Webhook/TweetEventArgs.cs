using System;

namespace Bots.Twitter
{
    public class TweetEventArgs : EventArgs
    {
        public Tweet Tweet { get; set; }
    }
}
