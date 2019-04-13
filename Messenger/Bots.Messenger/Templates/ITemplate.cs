using Newtonsoft.Json;

namespace Messenger.Bot.Templates
{
    using SendAPI;

    public interface ITemplate : IAttachment
    {
        [JsonProperty("template_type")]
        TemplateTypes TemplateType { get; }
    }
}
