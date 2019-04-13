using Microsoft.Extensions.Logging;
using System;

namespace EchoBot
{
    using Messenger.Bot;
    using static Messenger.Bot.Consts;

    class Program
    {
        static MessengerBot bot = new MessengerBot(80, AppSecret, PageToken, VerifyToken, LogLevel.Error);

        static void Main(string[] args)
        {
            Console.WriteLine("Starting the bot...");

            bot.MessageReceived += Bot_MessageReceived;

            bot.StartReceivingAsync();

            bot.WaitForShutdown();
        }

        private async static void Bot_MessageReceived(MessageEventArgs e)
        {
            Console.WriteLine($"\n{e.Sender}:{e.Message.Text}");

            await bot.SendMessageAsync(e.Sender, e.Message.Text);
        }
    }
}
