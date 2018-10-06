using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Messenger
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum QuickReplyTypes
    {
        [EnumMember(Value = "text")]
        Text,

        [EnumMember(Value = "location")]
        Location,

        [EnumMember(Value = "user_phone_number")]
        UserPhoneNumber,

        [EnumMember(Value = "user_email")]
        UserEmail
    }
}
