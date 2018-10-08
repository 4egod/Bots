using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Messenger.Templates
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TemplateTypes
    {
        [EnumMember(Value = "generic")]
        Generic,

        [EnumMember(Value = "list")]
        List,

        [EnumMember(Value = "button")]
        Button,

        [EnumMember(Value = "open_graph")]
        OpenGraph,

        [EnumMember(Value = "media")]
        Media,

        [EnumMember(Value = "receipt")]
        Receipt,

        [EnumMember(Value = "airline_boardingpass")]
        Airline
    }
}
