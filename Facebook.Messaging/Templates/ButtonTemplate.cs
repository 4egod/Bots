using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Facebook.Messaging.Templates
{
    using Buttons;

    public class ButtonTemplate : BaseTemplate
    {
        private string text;

        public override TemplateTypes TemplateType => TemplateTypes.Button;

        [JsonProperty("text")]
        public string Text
        {
            get => text;
            set
            {
                if (value.Length <= 640) text = value;
                else throw new ArgumentException(nameof(Text));
            }
        }

        [JsonProperty("buttons")]
        public List<IButton> Buttons { get; set; }

        [JsonProperty("sharable")]
        public bool Sharable { get; set; }
    }
}
