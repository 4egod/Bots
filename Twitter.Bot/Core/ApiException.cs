using System;

namespace Twitter.Bot
{
    using Models;

    public class ApiException : Exception
    {
        public ApiException(string message) : base(message) { }

        public ApiException(ApiError error) : base($"[{error.Code}]: {error.Message}") { }
    }
}
