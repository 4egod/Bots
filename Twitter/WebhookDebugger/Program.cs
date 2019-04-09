using Bots;
using Bots.Twitter;
using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebhookDebugger
{
    using System.Threading.Tasks;
    using static Consts;

    class Program
    {
        static TwitterBot bot = new TwitterBot(80, ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret, LogLevel.Warning);

        static bool RawDebug = false;

        static void Main(string[] args)
        {
            if (RawDebug)
            {
                bot.PostReceived += Bot_PostReceived;            
            }

            bot.InvalidPostReceived += Bot_InvalidPostReceived;
            bot.DirectMessageReceived += Bot_DirectMessageReceived;
            bot.Followed += Bot_Followed;
            bot.StartReceivingAsync();

            while (true)
            {
                Console.ReadLine();
                Console.Clear();
            }
        }

        private static async void Bot_Followed(FollowEventArgs e)
        {
            await Task.Run(async () =>
            {
                await Task.Delay(50);

                Console.WriteLine($"[Follow] {e.Source.Id} ({e.Source.ScreenName})");
            });
        }

        private static async void Bot_DirectMessageReceived(DirectMessageEventArgs e)
        {
            await Task.Run(async () =>
            {
                await Task.Delay(50);

                Console.WriteLine($"[Direct Message] {e.Message.Sender}: {e.Message.Text}");
            });
        }

        private static async void Bot_InvalidPostReceived(WebhookEventArgs e)
        {
            await Task.Run(async () =>
            {
                await Task.Delay(50);

                Console.WriteLine("Invalid POST received (invalid or empty signature)");
            });
        }

        private static async void Bot_PostReceived(WebhookEventArgs e)
        {
            if (!e.IsValid) return;

            await Task.Run(() =>
            {
                object o = JsonConvert.DeserializeObject(e.Body);
                string s = JsonConvert.SerializeObject(o, Formatting.Indented);
                Console.WriteLine("\n" + s);
            });
        }
    }
}
