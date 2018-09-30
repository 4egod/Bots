using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Facebook.Messaging
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ImageAspectRatio
    {
        [EnumMember(Value = "horizontal")]
        Horizontal,

        [EnumMember(Value = "square")]
        Square,
    }
}
