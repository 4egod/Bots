using System;

namespace Facebook.Bot
{
    using Messaging;
    using Webhook;

    public class BotClient
    {
        private MessagingClient messagingClient;

        private WebhookServer webhook;

        public BotClient(string appSecret, string pageToken, string verifyToken) : this(80, appSecret, pageToken, verifyToken) { }

        public BotClient(int webhookPort, string appSecret, string pageToken, string verifyToken)
        {
            messagingClient = new MessagingClient(pageToken);
            webhook = new WebhookServer(webhookPort, appSecret, verifyToken);
        }

        public void StartAsync()
        {
            webhook.StartAsync();
        }

        public void WaitForShutdown()
        {
            webhook.WaitForShutdown();
        }
    }
}
