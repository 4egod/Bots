using System;
using System.Collections.Generic;
using System.Text;

namespace Messenger
{
    using ProfileAPI;

    public class MessengerBot
    {
        //private MessagingClient messagingClient;

        //private WebhookServer webhook;

        private ProfileClient profileClient;

        public MessengerBot(string appSecret, string pageToken, string verifyToken) : this(80, appSecret, pageToken, verifyToken) { }

        public MessengerBot(int webhookPort, string appSecret, string pageToken, string verifyToken)
        {
            profileClient = new ProfileClient(pageToken);
            //messagingClient = new MessagingClient(pageToken);
            //webhook = new WebhookServer(webhookPort, appSecret, verifyToken);
        }

        public void StartAsync()
        {
            //webhook.StartAsync();
        }

        public void WaitForShutdown()
        {
            //webhook.WaitForShutdown();
        }
    }
}
