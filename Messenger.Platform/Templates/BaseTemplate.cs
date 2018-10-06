using Newtonsoft.Json;

namespace Messenger.Templates
{
    public abstract class BaseTemplate : ITemplate
    {
        [JsonIgnore]
        public AttachmentTypes AttachmentType => AttachmentTypes.Template;

        [JsonProperty("template_type")]
        public abstract TemplateTypes TemplateType { get; }
    }
}
