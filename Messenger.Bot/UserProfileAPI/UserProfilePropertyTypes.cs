using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Messenger.Bot.UserProfileAPI
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UserProfilePropertyTypes
    {
        [EnumMember(Value = "id")]
        Id,

        [EnumMember(Value = "name")]
        Name,

        [EnumMember(Value = "first_name")]
        FirstName,

        [EnumMember(Value = "last_name")]
        LastName,

        [EnumMember(Value = "profile_pic")]
        ProfilePic,

        [EnumMember(Value = "locale")]
        Locale,

        [EnumMember(Value = "timezone")]
        TimeZone,

        [EnumMember(Value = "male")]
        Gender,
    }
}
