using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Twitter.Bot
{
    using DirectMessagesAPI;

    public class TwitterBot : Webhook.WebhookServer
    {
#if DEBUG
        public const LogLevel DefaultLogLevel = LogLevel.Debug;
#else
        public const LogLevel DefaultLogLevel = LogLevel.Information;
#endif
        private bool isWebhookEnabled;
        private DirectMessagesApiClient directMessagesClient;

        public TwitterBot(int webhookPort, string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret, LogLevel logLevel = DefaultLogLevel) :
            base(webhookPort, consumerSecret, logLevel)
        {
            isWebhookEnabled = true;

            ConsumerKey = consumerKey;
            // ApiSecret assigned into webhook
            AccessToken = accessToken;
            AccessTokenSecret = accessTokenSecret;

            directMessagesClient = new DirectMessagesApiClient(consumerKey, consumerSecret, accessToken, accessTokenSecret); 
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

        public async Task<Message> SendDirectMessageAsync(long userId, string text)
        {
            return await SendDirectMessageAsync(userId, text, null);
        }

        public async Task<Message> SendDirectMessageAsync(long userId, string text, QuickReply quickReply)
        {
            var messageEvent = await directMessagesClient.SendDirectMessageAsync(userId, text, quickReply);

            return messageEvent.ToMessage();
        }
    }
}
