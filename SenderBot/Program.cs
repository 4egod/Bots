using System;

namespace SenderBot
{
    using static Messenger.Consts;
    using Messenger;

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
