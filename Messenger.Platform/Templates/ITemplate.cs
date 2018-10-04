using Newtonsoft.Json;

namespace Messenger.Templates
{
    using SendAPI;

    public interface ITemplate : IAttachment
    {
        [JsonProperty("template_type")]
        TemplateTypes TemplateType { get; }
    }
}
