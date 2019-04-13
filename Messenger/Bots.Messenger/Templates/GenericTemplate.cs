using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace Messenger.Bot.Templates
{
    using Buttons;

    public class GenericTemplate : BaseTemplate
    {
        public class Element
        {
            private List<IButton> buttons;

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("subtitle")]
            public string Subtitle { get; set; }

            [JsonProperty("image_url")]
            public string ImageUrl { get; set; }

            [JsonProperty("default_action")]
            public ElementAction DefaultAction { get; set; }

            [JsonProperty("buttons")]
            public List<IButton> Buttons
            {
                get => buttons;

                set
                {
                    if (value.Count > MaxButtonsCount || value.Count <= 0)
                    {
                        throw new ArgumentException(nameof(Buttons));
                    }

                    buttons = value;
                }
            }

            protected virtual int MaxButtonsCount => 3;
        }

        public override TemplateTypes TemplateType => TemplateTypes.Generic;

        [JsonProperty("elements")]
        public List<Element> Elements { get; set; }
    }
}
