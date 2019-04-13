using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Bots.Twitter
{
    using Api;

    public class TwitterBot : Webhook.WebhookServer
    {
#if DEBUG
        public const LogLevel DefaultLogLevel = LogLevel.Debug;
#else
        public const LogLevel DefaultLogLevel = LogLevel.Information;
#endif
        private bool isWebhookEnabled;
        private UsersClient usersClient;
        private DirectMessagesClient directMessagesClient;

        public TwitterBot(int webhookPort, string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret, LogLevel logLevel = DefaultLogLevel) :
            base(webhookPort, consumerSecret, logLevel)
        {
            isWebhookEnabled = true;

            ConsumerKey = consumerKey;
            // ApiSecret assigned into webhook
            AccessToken = accessToken;
            AccessTokenSecret = accessTokenSecret;

            usersClient = new UsersClient(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            directMessagesClient = new DirectMessagesClient(consumerKey, consumerSecret, accessToken, accessTokenSecret); 
        }

        public string ConsumerKey { get; private set; }

        public string AccessToken { get; private set; }

        public string AccessTokenSecret { get; private set; }

        public override void StartReceivingAsync()
        {
            if (!isWebhookEnabled)
            {
                throw new InvalidOperationException();
            }

            base.StartReceivingAsync();
        }

        public async Task<User> GetUserAsync(long userId)
        {
            return await usersClient.GetUserAsync(userId);
        }

        public async Task<Message> SendMessageAsync(long userId, string text)
        {
            return await SendMessageAsync(userId, text, null);
        }

        public async Task<Message> SendMessageAsync(long userId, string text, QuickReply quickReply)
        {
            var messageEvent = await directMessagesClient.SendMessageAsync(userId, text, quickReply);

            return messageEvent.ToMessage();
        }
    }
}
