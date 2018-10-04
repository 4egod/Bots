using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messenger.SendAPI
{
    public class SendClient : BaseApiClient
    {
        public SendClient(string pageToken) : base(pageToken)
        {
        }

        public override string ApiUri => $"https://graph.facebook.com/v{ApiVersion}/me/messages?access_token={PageToken}";

        public async Task<string> SendMessageAsync(ulong userId, string text)
        {
            return await SendMessageAsync(userId, text, null);
        }

        public async Task<string> SendMessageAsync(ulong userId, string text, List<QuickReply> quickReplies)
        {
            if (text == null)
            {
                throw new NullReferenceException(nameof(text));
            }

            Recipient recipient = new Recipient() { id = userId.ToString() };
            Message message = new Message() { text = text };
            message.quick_replies = quickReplies;
            MessageContainer container = new MessageContainer()
            {
                recipient = recipient,
                message = message
            };

            var response = await PostAsync<Response>(container, ApiUri);
            return response.message_id;
        }

        public async Task<string> RequestUserLocationAsync(ulong userId, string message)
        {
            List<QuickReply> qrl = new List<QuickReply>();
            qrl.Add(new QuickReply()
            {
                QuickReplyType = QuickReplyTypes.Location
            });

            return await SendMessageAsync(userId, message, qrl);
        }

        public async Task<string> RequestUserPhoneNumberAsync(ulong userId, string message)
        {
            List<QuickReply> qrl = new List<QuickReply>();
            qrl.Add(new QuickReply()
            {
                QuickReplyType = QuickReplyTypes.UserPhoneNumber
            });

            return await SendMessageAsync(userId, message, qrl);
        }

        public async Task<string> RequestUserEmailAsync(ulong userId, string message)
        {
            List<QuickReply> qrl = new List<QuickReply>();
            qrl.Add(new QuickReply()
            {
                QuickReplyType = QuickReplyTypes.UserEmail
            });

            return await SendMessageAsync(userId, message, qrl);
        }

        public async Task<string> SendAttachment<T>(ulong userId, T attachment)
        {
            Recipient recipient = new Recipient() { id = userId.ToString() };
            Message<T> message = new Message<T>() { Attachment = attachment };
            MessageContainer container = new MessageContainer()
            {
                recipient = recipient,
                message = message
            };

            var response = await PostAsync<Response>(container, ApiUri);
            return response.message_id;
        }


    }
}
