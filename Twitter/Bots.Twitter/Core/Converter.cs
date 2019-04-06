using System;
using System.Collections.Generic;
using System.Text;

namespace Bots.Twitter
{
    internal static class Converter
    {
        public static long ToTwitterTimestamp(this DateTime dateTime)
        {
            return (long)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
        }

        public static DateTime FromTwitterTimestamp(this long timestamp)
        {
            DateTime result = new DateTime(1970, 1, 1);
            result = result.AddMilliseconds(timestamp);
            return result;
        }
    }
}
