using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    using Microsoft.Extensions.Logging;
    using ProfileAPI;
    using SendAPI;

    public class MessengerBot : Webhook.WebhookServer
    {
      
        private ProfileClient profileClient;
        private SendClient sendClient;

        public MessengerBot(string appSecret, string pageToken, string verifyToken) : this(80, appSecret, pageToken, verifyToken) { }

        public MessengerBot(int webhookPort, string appSecret, string pageToken, string verifyToken) : base(webhookPort, appSecret, verifyToken)
        {
            PageToken = pageToken;
            profileClient = new ProfileClient(pageToken);
            sendClient = new SendClient(pageToken);

        }

        public string PageToken { get; set; }

        public override void StartAsync()
        {
            base.StartAsync();
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
                return MessageResult.Failed;
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
                return MessageResult.Failed;
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
                return MessageResult.Failed;
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
                return MessageResult.Failed;
            }
        }

        //public async Task<MessageResult> SendAttachment<T>(ulong userId, T attachment) where T : IAttachment
        //{
        //    return await SendAttachment<T>(userId.ToString(), attachment);
        //}

        //public async Task<MessageResult> SendAttachment<T>(string userId, T attachment) where T: IAttachment
        //{
        //    try
        //    {
        //        return await sendClient.SendAttachment<T>(userId, attachment);
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.LogError(e, e.Message);
        //        return MessageResult.Failed;
        //    }
        //}







    }
}
