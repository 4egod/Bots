using Microsoft.Extensions.Logging;
using System;

namespace WebhookDebugBot
{
    using Messenger.Bot;
    using static Messenger.Bot.Consts;

    class Program
    {
        static MessengerBot bot = new MessengerBot(80, AppSecret, PageToken, VerifyToken, LogLevel.Error);

        static void Main(string[] args)
        {
            Console.WriteLine("Starting the bot...");

            bot.OnPost += Bot_OnPost;
            bot.OnMessage += Bot_OnMessage;
            bot.OnPostback += Bot_OnPostback;

            bot.StartReceivingAsync();

            bot.WaitForShutdown();
        }

        private static void Bot_OnPostback(PostbackEventArgs e)
        {
            Console.WriteLine($"\nPOSTBACK:{e.Sender}:{e.Postback.Title}:{e.Postback.Payload}");
        }

        private static void Bot_OnMessage(MessageEventArgs e)
        {
            Console.WriteLine($"\nMESSAGE:{e.Sender}:{e.Message.Text}");

            if (e.Message.QuickReply != null)
            {
                Console.WriteLine($"QUICK_REPLY:{e.Message.QuickReply.Payload}");
            }
        }

        private static void Bot_OnPost(PostEventArgs e)
        {
            Console.WriteLine($"\nPOST:\n{e.Body}");
        }
    }
}
