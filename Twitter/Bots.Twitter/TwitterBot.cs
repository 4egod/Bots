using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Bots.Twitter
{
    using Api;

    public class TwitterBot : Webhook.WebhookServer
    {
#if DEBUG
        public const LogLevel DefaultLogLevel = LogLevel.Information;
#else
        public const LogLevel DefaultLogLevel = LogLevel.Warning;
#endif
        private bool isWebhookEnabled;
        private UsersClient usersClient;
        private DirectMessagesClient directMessagesClient;
        private WelcomeMessageClient welcomeMessageClient;

        public TwitterBot(int webhookPort, string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret, LogLevel logLevel = DefaultLogLevel) :
            this(webhookPort, 0, consumerKey, consumerSecret, accessToken, accessTokenSecret, logLevel)
        {
        }

        public TwitterBot(int webhookPort, long recipient, string consumerKey, string consumerSecret, string accessToken, 
            string accessTokenSecret, LogLevel logLevel = DefaultLogLevel) : base(webhookPort, recipient, consumerSecret, logLevel)
        {
            isWebhookEnabled = true;

            ConsumerKey = consumerKey;
            // ApiSecret assigned into webhook
            AccessToken = accessToken;
            AccessTokenSecret = accessTokenSecret;

            usersClient = new UsersClient(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            directMessagesClient = new DirectMessagesClient(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            welcomeMessageClient = new WelcomeMessageClient(consumerKey, consumerSecret, accessToken, accessTokenSecret);
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

        public async Task<User> GetUserAsync(string screenName)
        {
            return await usersClient.GetUserAsync(screenName);
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

        public async Task<WelcomeMessage> CreateWelcomeMessageAsync(string text)
        {
            return await CreateWelcomeMessageAsync(text, null);
        }

        public async Task<WelcomeMessage> CreateWelcomeMessageAsync(string text, QuickReply quickReply)
        {
            return (await welcomeMessageClient.CreateWelcomeMessageAsync(null, text, quickReply)).ToWelcomeMessage();
        }


    }
}
