using System;

namespace Messenger
{
    public class ApiException : Exception
    {
        public ApiException(ApiError error) : base($"[{error.Type}:{error.Code}]: {error.Message}") { }
    }
}
