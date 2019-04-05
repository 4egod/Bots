using System;

namespace ConsoleApp1
{
    using Twitter.Bot.Webhook;
    using Microsoft.Extensions.Logging;
    using Twitter.Bot;
    using Twitter.Bot.DirectMessagesAPI;
    using Twitter.Bot.Models;

    class Program
    {
        static void Main(string[] args)
        {
            TwitterBot bot = new TwitterBot(80,
                "siVUFPTf9F6D3ahQhG5D8WOLC",
                "EFqBhAOIduv3tGwVag4FDFCgXVwxGbGXAKRaZukKzxfBYX6Obi",
                "223338987-GNcGdbiT115WfYUrRRy2ZYz0fUOxpeIcJUTc6bDn",
                "OryVxtHpAgIn4uV9Xw2JXBwffidvtYVq1Asz8hqpeNiyp",
                LogLevel.Error);
            //ws.PostReceived += Ws_PostReceived;
            bot.StartReceivingAsync();
            bot.WaitForShutdown();

            //DirectMessagesApiClient dm = new DirectMessagesApiClient(
            //    "siVUFPTf9F6D3ahQhG5D8WOLC",
            //    "EFqBhAOIduv3tGwVag4FDFCgXVwxGbGXAKRaZukKzxfBYX6Obi",
            //    "223338987-GNcGdbiT115WfYUrRRy2ZYz0fUOxpeIcJUTc6bDn",
            //    "OryVxtHpAgIn4uV9Xw2JXBwffidvtYVq1Asz8hqpeNiyp");

            QuickReply qr = new QuickReply();
            qr.Options = new System.Collections.Generic.List<QuickReplyOption>();
            qr.Options.Add(new QuickReplyOption() { Label = "1", Metadata = "1" });
            qr.Options.Add(new QuickReplyOption() { Label = "2", Metadata = "2" });

            //var m = dm.SendDirectMessageAsync(1112683711672262656, "test_message", qr).Result;


            //Console.WriteLine("Hello World!");
            while (true)
            {
                Console.ReadLine();
                Console.Clear();
            }
            

        }


    }
}
