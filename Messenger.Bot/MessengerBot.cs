using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messenger.Bot
{
    using BroadcastAPI;
    using Menu;
    using ProfileAPI;
    using SendAPI;

    public class MessengerBot : Webhook.WebhookServer
    {
#if DEBUG
        public const LogLevel DefaultLogLevel = LogLevel.Debug;
#else
        public const LogLevel DefaultLogLevel = LogLevel.Information;
#endif

        private ProfileApiClient profileClient;
        private SendApiClient sendClient;
        private BroadcastApiClient broadcastClient;

        bool isWebhookEnabled;

        public MessengerBot(string pageToken)
        {
            isWebhookEnabled = false;

            PageToken = pageToken;
            profileClient = new ProfileApiClient(pageToken);
            sendClient = new SendApiClient(pageToken);
            broadcastClient = new BroadcastApiClient(pageToken);
        }

        public MessengerBot(string appSecret, string pageToken, string verifyToken) :
            this(80, appSecret, pageToken, verifyToken, DefaultLogLevel)
        {
        }

        public MessengerBot(int webhookPort, string appSecret, string pageToken, string verifyToken) :
            base(webhookPort, appSecret, verifyToken, DefaultLogLevel)
        {
        }

        public MessengerBot(int webhookPort, string appSecret, string pageToken, string verifyToken, LogLevel logLevel = DefaultLogLevel) :
            base(webhookPort, appSecret, verifyToken, logLevel)
        {
            isWebhookEnabled = true;

            PageToken = pageToken;
            profileClient = new ProfileApiClient(pageToken);
            sendClient = new SendApiClient(pageToken);
            broadcastClient = new BroadcastApiClient(pageToken);
        }

        public string PageToken { get; set; }

        public override void StartReceivingAsync()
        {
            if (!isWebhookEnabled)
            {
                throw new InvalidOperationException();
            }

            base.StartReceivingAsync();
        }




        public async Task<bool> SetStartButtonPostback(string payload)
        {
            try
            {
                return await profileClient.SetStartButtonPostback(payload);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return false;
            }
        }

        public async Task<bool> SetGreeting(string text)
        {
            try
            {
                return await profileClient.SetGreeting(text);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return false;
            }
        }

        public async Task<bool> SetPersistentMenu(PersistentMenu menu)
        {
            try
            {
                return await profileClient.SetPersistentMenu(menu);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return false;
            }
        }



        public async Task<MessageResult> SendMessageAsync(ulong userId, string text)
        {
            return await SendMessageAsync(userId, text, null);
        }

        public async Task<MessageResult> SendMessageAsync(string userId, string text)
        {
            return await SendMessageAsync(userId, text, null);
        }

        public async Task<MessageResult> SendMessageAsync(ulong userId, string text, List<QuickReply> quickReplies)
        {
            return await SendMessageAsync(userId.ToString(), text, quickReplies);
        }

        public async Task<MessageResult> SendMessageAsync(string userId, string text, List<QuickReply> quickReplies)
        {
            try
            {
                return await sendClient.SendMessageAsync(userId, text, quickReplies);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BaseResult.CreateFailed<MessageResult>();
            }
        }

        public async Task<MessageResult> RequestUserLocationAsync(ulong userId, string message)
        {
            return await RequestUserLocationAsync(userId.ToString(), message); 
        }

        public async Task<MessageResult> RequestUserLocationAsync(string userId, string message)
        {
            try
            {
                return await sendClient.RequestUserLocationAsync(userId, message);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BaseResult.CreateFailed<MessageResult>();
            }
        }

        public async Task<MessageResult> RequestUserPhoneNumberAsync(ulong userId, string message)
        {
            return await RequestUserPhoneNumberAsync(userId.ToString(), message);
        }

        public async Task<MessageResult> RequestUserPhoneNumberAsync(string userId, string message)
        {
            try
            {
                return await sendClient.RequestUserPhoneNumberAsync(userId, message);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BaseResult.CreateFailed<MessageResult>();
            }
        }

        public async Task<MessageResult> RequestUserEmailAsync(ulong userId, string message)
        {
            return await RequestUserEmailAsync(userId.ToString(), message);
        }

        public async Task<MessageResult> RequestUserEmailAsync(string userId, string message)
        {
            try
            {
                return await sendClient.RequestUserEmailAsync(userId, message);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BaseResult.CreateFailed<MessageResult>();
            }
        }

        public async Task<MessageResult> SendAttachment<T>(ulong userId, Attachment<T> attachment) where T : IAttachment
        {
            return await SendAttachment<T>(userId.ToString(), attachment);
        }

        public async Task<MessageResult> SendAttachment<T>(string userId, Attachment<T> attachment) where T : IAttachment
        {
            try
            {
                return await sendClient.SendAttachment<T>(userId, attachment);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BaseResult.CreateFailed<MessageResult>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns>
        /// Broadcast message ID that can be used by <see cref="SendMessageAsync(string, string)"./>
        /// </returns>
        public async Task<BroadcastMessageResult> CreateBroadcastMessageAsync(string text)
        {
            return await CreateBroadcastMessageAsync(text, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="quickReplies"></param>
        /// <returns>
        /// 
        /// </returns>
        public async Task<BroadcastMessageResult> CreateBroadcastMessageAsync(string text, List<QuickReply> quickReplies)
        {
            try
            {
                return await broadcastClient.CreateMessageAsync(text, quickReplies);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BaseResult.CreateFailed<BroadcastMessageResult>();
            }
        }

        public async Task<BroadcastResult> BroadcastMessageAsync(BroadcastMessageResult message, NotificationTypes notification)
        {
            try
            {
                return await broadcastClient.BroadcastMessageAsync(message, notification);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BaseResult.CreateFailed<BroadcastResult>();
            }
        }

        public async Task<BroadcastResult> BroadcastMessageAsync(BroadcastMessageResult message, NotificationTypes notification, DateTime scheduleTime)
        {
            try
            {
                return await broadcastClient.BroadcastMessageAsync(message, notification, scheduleTime);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BaseResult.CreateFailed<BroadcastResult>();
            }
        }

        public async Task<bool> CancelScheduledBroadcast(BroadcastResult broadcast)
        {
            try
            {
                return await broadcastClient.CancelScheduledBroadcast(broadcast);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return false;
            }
        }

    }
}
