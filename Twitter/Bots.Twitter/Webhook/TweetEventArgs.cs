using System;

namespace Bots.Twitter
{
    public class TweetEventArgs : BaseEventArgs
    {
        public Tweet Tweet { get; set; }
    }
}
