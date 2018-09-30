using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Facebook.Messaging
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AttachmentTypes
    {
        [EnumMember(Value = "image")]
        Image,

        [EnumMember(Value = "audio")]
        Audio,

        [EnumMember(Value = "video")]
        Video,

        [EnumMember(Value = "file")]
        File,

        [EnumMember(Value = "template")]
        Template
    }
}
