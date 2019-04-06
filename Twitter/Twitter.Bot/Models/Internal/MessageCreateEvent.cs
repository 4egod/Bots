using Newtonsoft.Json;

namespace Bots.Twitter.Models
{
    internal class MessageCreateEvent : Event
    {
        public override string Type => "message_create";

        [JsonProperty("message_create")]
        public MessageCreateData Data { get; set; }

        public Message ToMessage()
        {
            Message m = new Message
            {
                Id = Id,
                Timestamp = Timestamp,
                Sender = Data.Sender,
                Recipient = Data.Target.RecipientId,
                Text = Data.Data.Text,
                QuickReplyResponse = Data.Data.QuickReplyResponse
            };

            return m;
        }
    }
}
