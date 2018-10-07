using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WebhookDebugBot
{
    using static Messenger.Consts;
    using Messenger;  

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

        private async static Task Bot_OnPostback(PostbackEventArgs e)
        {
            Console.WriteLine($"\nPOSTBACK:{e.Sender}:{e.Postback.Title}:{e.Postback.Payload}");

            await Task.CompletedTask;
        }

        private async static Task Bot_OnMessage(MessageEventArgs e)
        {
            Console.WriteLine($"\nMESSAGE:{e.Sender}:{e.Message.Text}");

            if (e.Message.QuickReply != null)
            {
                Console.WriteLine($"QUICK_REPLY:{e.Message.QuickReply.Payload}");
            }

            await Task.CompletedTask;
        }

        private async static Task Bot_OnPost(PostEventArgs e)
        {
            Console.WriteLine($"\nPOST:\n{e.Body}");

            await Task.CompletedTask;
        }
    }
}
