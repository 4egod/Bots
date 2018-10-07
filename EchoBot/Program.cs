using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static Messenger.Consts;

namespace EchoBot
{
    using Messenger;

    class Program
    {
        static MessengerBot bot = new MessengerBot(AppSecret, PageToken, VerifyToken);

        static void Main(string[] args)
        {
            Console.WriteLine("Starting the bot...");

            SetupBotProfile();

            bot.OnPost += Bot_OnPost;
            bot.StartReceivingAsync();

            //SendMessages();

            bot.WaitForShutdown();
        }

        private async static Task Bot_OnPost(Messenger.Webhook.PostEventArgs e)
        {
            Console.WriteLine($"POST: {e.Body}");

            await Task.CompletedTask;
        }

        static async void SetupBotProfile()
        {
            await bot.SetStartButtonPostback("#get_started_button_postback#");
        }

        static async void SendMessages()
        {
            await bot.SendMessageAsync(UserId, "Test message");

        }
    }
}
