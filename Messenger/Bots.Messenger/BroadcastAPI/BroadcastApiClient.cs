using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messenger.Bot.BroadcastAPI
{
    public class BroadcastApiClient : BaseApiClient
    {
        public BroadcastApiClient(string pageToken) : base(pageToken)
        {
        }

        public override string ApiUri => $"https://graph.facebook.com/v{ApiVersion}/me/message_creatives?access_token={PageToken}";

        public string BroadcastApiUri => $"https://graph.facebook.com/v{ApiVersion}/me/broadcast_messages?access_token={PageToken}";

        public async Task<BroadcastMessageResult> CreateMessageAsync(string text)
        {
            return await CreateMessageAsync(text, null);
        }

        public async Task<BroadcastMessageResult> CreateMessageAsync(string text, List<QuickReply> quickReplies)
        {
            BroadcastMessage message = new BroadcastMessage() { Text = text };
            message.QuickReplies = quickReplies;

            BroadcastMessageContainer container = new BroadcastMessageContainer()
            {
                Messages = new List<BroadcastMessage>()
            };

            container.Messages.Add(message);

            var result = await PostAsync<BroadcastMessageResult>(container, ApiUri);

            return result;
        }

        public async Task<BroadcastResult> BroadcastMessageAsync(BroadcastMessageResult message, NotificationTypes notification)
        {
            BroadcastContainer container = new BroadcastContainer()
            {
                CreativeMessageId = message.Id,
                NotificationType = notification
            };

            var result = await PostAsync<BroadcastResult>(container, BroadcastApiUri);

            return result;
        }

        public async Task<BroadcastResult> BroadcastMessageAsync(BroadcastMessageResult message, NotificationTypes notification, DateTime scheduleTime)
        {
            BroadcastContainer container = new BroadcastContainer()
            {
                CreativeMessageId = message.Id,
                NotificationType = notification,
                ScheduleTime = scheduleTime.ToUnixTimestamp().ToString()
            };

            var result = await PostAsync<BroadcastResult>(container, BroadcastApiUri);

            return result;
        }

        public async Task<bool> CancelScheduledBroadcast(BroadcastResult broadcast)
        {
            string apiUri = $"https://graph.facebook.com/v{ApiVersion}/{broadcast.Id}?access_token={PageToken}";
            CancelContainer container = new CancelContainer();

            var result = await PostAsync<CancelResult>(container, apiUri);

            return result.Result;
        }
    }
}
