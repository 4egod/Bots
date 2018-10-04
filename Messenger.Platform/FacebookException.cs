using System;

namespace Messenger
{
    public class FacebookException : Exception
    {
        public FacebookException(ApiError error) : base($"[{error.Type}:{error.Code}]: {error.Message}") { }
    }
}
