using System;

namespace EchoBot
{
    using static Facebook.Consts;
    using Facebook.Bot;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            BotClient bot = new BotClient(AppSecret, PageToken, VerifyToken);
            bot.StartAsync();
            bot.WaitForShutdown();
        }
    }
}
