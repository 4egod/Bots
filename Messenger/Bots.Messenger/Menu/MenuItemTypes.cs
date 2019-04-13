using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Messenger.Bot.Menu
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MenuItemTypes
    {
        [EnumMember(Value = "web_url")]
        UrlMenuItem,

        [EnumMember(Value = "postback")]
        PostbackMenuItem,

        [EnumMember(Value = "nested")]
        NestedMenuItem
    }
}
