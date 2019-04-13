using System;

namespace Bots.Twitter
{
    public class LikeEventArgs : EventArgs
    {
        public string Id { get; set; }

        public DateTime Timestamp { get; set; }

        public Tweet Tweet { get; set; }

        public User User { get; set; }
    }
}
