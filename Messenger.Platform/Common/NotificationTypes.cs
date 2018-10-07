using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Messenger
{
    /// <summary>
    /// Push notification types
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NotificationTypes
    {
        /// <summary>
        /// Sound/vibration
        /// </summary>
        [EnumMember(Value = "REGULAR")]
        Regular,

        /// <summary>
        /// On-screen notification only
        /// </summary>
        [EnumMember(Value = "SILENT_PUSH")]
        SilentPush,

        /// <summary>
        /// No notification
        /// </summary>
        [EnumMember(Value = "NO_PUSH")]
        NoPush
    }
}
