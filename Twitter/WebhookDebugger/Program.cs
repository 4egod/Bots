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

        static void Main(string[] args)
        {
            bot.PostReceived += Bot_PostReceived; 
            bot.InvalidPostReceived += Bot_InvalidPostReceived;
            bot.StartReceivingAsync();

            while (true)
            {
                Console.ReadLine();
                Console.Clear();
            }
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
