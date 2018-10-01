using Newtonsoft.Json;

namespace Facebook.Messaging.Templates
{
    public class ElementAction
    {
        [JsonProperty("type")]
        public const string Type = "web_url";

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("messenger_extensions")]
        public bool MessengerExtensions { get; set; }

        [JsonProperty("webview_height_ratio")]
        public WebViewHeightRatio WebViewHeightRatio { get; set; }

        [JsonProperty("fallback_url")]
        public string FallbackUrl { get; set; }
    }
}
