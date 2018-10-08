using Newtonsoft.Json;
using System;

namespace Messenger.Bot
{
    public abstract class BaseResult
    {
        public abstract string Id { get; set; }

        [JsonIgnore]
        public bool Result => !string.IsNullOrEmpty(Id);

        public static T CreateFailed<T>() where T: BaseResult => Activator.CreateInstance<T>();
    }
}
