using System;

namespace SenderBot
{
    using static Messenger.Bot.Consts;
    using Messenger.Bot;

    class Program
    {
        static MessengerBot bot = new MessengerBot(PageToken);

        static void Main(string[] args)
        {
            SendMessages();

            Console.WriteLine("Press anykey for exit...");
            Console.ReadKey();
        }

        static async void SendMessages()
        {
            await bot.SendMessageAsync(UserId, "Test message");
        }
    }
}
