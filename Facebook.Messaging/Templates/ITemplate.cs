using Newtonsoft.Json;

namespace Facebook.Messaging.Templates
{
    public interface ITemplate : IAttachment
    {
        [JsonProperty("template_type")]
        TemplateTypes TemplateType { get; }
    }
}
