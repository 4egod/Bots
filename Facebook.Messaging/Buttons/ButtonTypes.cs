using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Facebook.Messaging.Buttons
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ButtonTypes
    {
        [EnumMember(Value = "web_url")]
        UrlButton,

        [EnumMember(Value = "postback")]
        PostbackButton,

        [EnumMember(Value = "element_share")]
        ShareButton,

        [EnumMember(Value = "payment")]
        BuyButton,

        [EnumMember(Value = "phone_number")]
        CallButton,

        [EnumMember(Value = "account_link")]
        LogInButton,
        
        [EnumMember(Value = "account_unlink")]
        LogOutButton,

        [EnumMember(Value = "game_play")]
        PlayGameButton,
    }
}
