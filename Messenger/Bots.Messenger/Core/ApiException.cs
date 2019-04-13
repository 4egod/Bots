using System;

namespace Messenger.Bot
{
    public class ApiException : Exception
    {
        public ApiException(ApiError error) : base($"[{error.Type}:{error.Code}]: {error.Message}") { }
    }
}
