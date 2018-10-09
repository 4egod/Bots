using Microsoft.Extensions.Logging;
using System;

namespace WebhookDebugBot
{
    using Messenger.Bot;
    using static Messenger.Bot.Consts;

    class Program
    {
        static MessengerBot bot = new MessengerBot(80, AppSecret, PageToken, VerifyToken, LogLevel.Warning);

        static void Main(string[] args)
        {
            Console.WriteLine("Starting the bot...");

            bot.PostReceived += Bot_PostReceived;
            bot.PostFailed += Bot_PostFailed;
            bot.MessageReceived += Bot_MessageReceived;
            bot.PostbackReceived += Bot_PostbackReceived;

            bot.StartReceivingAsync();

            bot.WaitForShutdown();
        }

        private static void Bot_PostbackReceived(PostbackEventArgs e)
        {
            Console.WriteLine($"\nPOSTBACK:{e.Sender}:{e.Postback.Title}:{e.Postback.Payload}");
        }

        private static void Bot_MessageReceived(MessageEventArgs e)
        {
            Console.WriteLine($"\nMESSAGE:{e.Sender}:{e.Message.Text}");

            if (e.Message.QuickReply != null)
            {
                Console.WriteLine($"QUICK_REPLY:{e.Message.QuickReply.Payload}");
            }
        }

        private static void Bot_PostFailed(PostEventArgs e)
        {
            Console.WriteLine($"\nVRONG POST:\n{e.Body}");
        }

        private static void Bot_PostReceived(PostEventArgs e)
        {
            Console.WriteLine($"\nPOST:\n{e.Body}");
        }
    }
}
