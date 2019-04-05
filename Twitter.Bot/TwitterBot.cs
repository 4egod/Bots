using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Twitter.Bot
{
    public class TwitterBot : Webhook.WebhookServer
    {
#if DEBUG
        public const LogLevel DefaultLogLevel = LogLevel.Debug;
#else
        public const LogLevel DefaultLogLevel = LogLevel.Information;
#endif

        private bool isWebhookEnabled;

        public TwitterBot(int webhookPort, string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret, LogLevel logLevel = DefaultLogLevel) :
            base(webhookPort, consumerSecret, logLevel)
        {
            isWebhookEnabled = true;

            ConsumerKey = consumerKey; 
            // ApiSecret assigned into webhook

            AccessToken = accessToken;
            AccessTokenSecret = accessTokenSecret;

            //PageToken = pageToken;
            //profileClient = new ProfileApiClient(pageToken);
            //sendClient = new SendApiClient(pageToken);
            //broadcastClient = new BroadcastApiClient(pageToken);
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
    }
}
