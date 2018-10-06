using System;

namespace EchoBot
{
    using static Messenger.Consts;
    using Messenger;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            MessengerBot bot = new MessengerBot(AppSecret, PageToken, VerifyToken);
            bot.StartAsync();

            bot.SendMessageAsync(UserId, "Test message").Wait();

            bot.WaitForShutdown();
        }
    }
}
