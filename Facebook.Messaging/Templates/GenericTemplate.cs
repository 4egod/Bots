using Newtonsoft.Json;
using System.Collections.Generic;

namespace Facebook.Messaging.Templates
{
    using Buttons;

    public class GenericTemplate : BaseTemplate
    {
        public class Element
        {
            public class Action
            {
                [JsonProperty("type")]
                public const string Type = "web_url";

                [JsonProperty("url")]
                public string Url { get; set; }

                [JsonProperty("messenger_extensions")]
                public bool MessengerExtensions { get; set; }

                [JsonProperty("webview_height_ratio")]
                public WebViewHeightRatio WebViewHeightRatio { get; set; }
            }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("subtitle")]
            public string Subtitle { get; set; }

            [JsonProperty("image_url")]
            public string ImageUrl { get; set; }

            [JsonProperty("default_action")]
            public Action DefaultAction { get; set; }

            [JsonProperty("buttons")]
            public List<IButton> Buttons { get; set; }
        }

        public override TemplateTypes TemplateType => TemplateTypes.Generic;

        [JsonProperty("elements")]
        public List<Element> Elements { get; set; }
    }
}
