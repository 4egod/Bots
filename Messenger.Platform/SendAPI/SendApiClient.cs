using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messenger.SendAPI
{
    public class SendApiClient : BaseApiClient
    {
        public SendApiClient(string pageToken) : base(pageToken)
        {
        }

        public override string ApiUri => $"https://graph.facebook.com/v{ApiVersion}/me/messages?access_token={PageToken}";

        public async Task<MessageResult> SendMessageAsync(string userId, string text)
        {
            return await SendMessageAsync(userId, text, null);
        }

        public async Task<MessageResult> SendMessageAsync(string userId, string text, List<QuickReply> quickReplies)
        {
            if (text == null)
            {
                throw new NullReferenceException(nameof(text));
            }

            Recipient recipient = new Recipient() { Id = userId };
            Message message = new Message() { Text = text };
            message.QuickReplies = quickReplies;
            MessageContainer container = new MessageContainer()
            {
                recipient = recipient,
                message = message
            };

            var response = await PostAsync<MessageResult>(container, ApiUri);

            return response;
        }

        public async Task<MessageResult> RequestUserLocationAsync(string userId, string message)
        {
            List<QuickReply> qrl = new List<QuickReply>();
            qrl.Add(new QuickReply()
            {
                QuickReplyType = QuickReplyTypes.Location
            });

            return await SendMessageAsync(userId, message, qrl);
        }

        public async Task<MessageResult> RequestUserPhoneNumberAsync(string userId, string message)
        {
            List<QuickReply> qrl = new List<QuickReply>();
            qrl.Add(new QuickReply()
            {
                QuickReplyType = QuickReplyTypes.UserPhoneNumber
            });

            return await SendMessageAsync(userId, message, qrl);
        }

        public async Task<MessageResult> RequestUserEmailAsync(string userId, string message)
        {
            List<QuickReply> qrl = new List<QuickReply>();
            qrl.Add(new QuickReply()
            {
                QuickReplyType = QuickReplyTypes.UserEmail
            });

            return await SendMessageAsync(userId, message, qrl);
        }

        public async Task<MessageResult> SendAttachment<T>(string userId, Attachment<T> attachment) where T: IAttachment
        {
            Recipient recipient = new Recipient() { Id = userId };
            Message<T> message = new Message<T>() { Attachment = attachment };
            MessageContainer container = new MessageContainer()
            {
                recipient = recipient,
                message = message
            };

            var response = await PostAsync<MessageResult>(container, ApiUri);

            return response;
        }


    }
}
