using Newtonsoft.Json;

namespace Facebook.Messaging
{
    public class Attachment<T> : Attachment where T: IAttachment
    {
        [JsonProperty("type")]
        public override AttachmentTypes AttachmentType => Payload.AttachmentType;

        [JsonProperty("payload")]
        public T Payload { get; set; }
    }

    public abstract class Attachment
    {
        [JsonProperty("type")]
        public abstract AttachmentTypes AttachmentType { get; }
    }
}
