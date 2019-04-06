using System;
using Bots.Twitter;

namespace EchoBot
{
    using static Consts;

    class Program
    {
        static TwitterBot bot = new TwitterBot(80, ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret);

        static void Main(string[] args)
        {
            Console.WriteLine("Starting the bot...");
            bot.MessageReceived += Bot_MessageReceived;
            bot.StartReceivingAsync();
            bot.WaitForShutdown();
        }

        private async static void Bot_MessageReceived(MessageEventArgs e)
        {
            Console.WriteLine($"{e.Message.Sender}: {e.Message.Text}");

            if (e.Message.Sender != UserId)
            {
                await bot.SendDirectMessageAsync(e.Message.Sender, e.Message.Text);
            }
        }
    }
}
