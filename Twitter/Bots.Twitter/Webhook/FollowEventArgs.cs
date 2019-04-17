using System;

namespace Bots.Twitter
{
    public enum FollowType
    {
        Follow,
        Unfollow
    }

    public class FollowEventArgs : BaseEventArgs
    {
        public DateTime Timestamp { get; set; }

        public FollowType Type { get; set; }

        public User Target { get; set; }

        public User Source { get; set; }
    }
}
