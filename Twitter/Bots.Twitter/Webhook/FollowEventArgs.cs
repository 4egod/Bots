using System;

namespace Bots.Twitter
{
    public class FollowEventArgs : EventArgs
    {
        public DateTime Timestamp { get; set; }

        public User Target { get; set; }

        public User Source { get; set; }
    }
}
