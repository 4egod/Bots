using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Facebook.Messaging.Templates
{
    using Buttons;

    public class ListTemplate : BaseTemplate
    {
        public class Element : GenericTemplate.Element
        {
            protected override int MaxButtonsCount => 1;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum ElementStyle
        {
            [EnumMember(Value = "compact")]
            Compact,

            [EnumMember(Value = "large")]
            Large
        }

        public override TemplateTypes TemplateType => TemplateTypes.List;

        [JsonProperty("top_element_style")]
        public ElementStyle TopElementStyle { get; set; }

        [JsonProperty("buttons")]
        public List<IButton> Buttons { get; set; }

        [JsonProperty("elements")]
        public List<Element> Elements { get; set; }

        [JsonProperty("sharable")]
        public bool Sharable { get; set; }
    }
}
