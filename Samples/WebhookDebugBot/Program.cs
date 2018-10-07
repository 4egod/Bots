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

            bot.StartReceivingAsync();

            bot.WaitForShutdown();
        }

        private async static Task Bot_OnPost(PostEventArgs e)
        {
            Console.WriteLine($"\n{e.Body}");

            await Task.CompletedTask;
        }
    }
}
