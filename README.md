|   Library   |   Version   |   Downloads   |
|---------|:----------:|:----------:|
| **Bots.Twitter** | [![NuGet](https://img.shields.io/nuget/v/Bots.Twitter.svg)](https://www.nuget.org/packages/Bots.Twitter) | [![NuGet](https://img.shields.io/nuget/dt/Bots.Twitter.svg)](https://www.nuget.org/packages/Bots.Twitter) |
| **Bots.Messenger** |[![NuGet](https://img.shields.io/nuget/v/Messenger.Bot.svg)](https://www.nuget.org/packages/Messenger.Bot)|[![NuGet](https://img.shields.io/nuget/dt/Messenger.Bot.svg)](https://www.nuget.org/packages/Messenger.Bot)|


# ![GitHub Logo](https://github.com/4egod/Bots/raw/master/Twitter/Logo.Small.png) Bots.Twitter

### How to use:

```csharp
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
            bot.OnMessage += Bot_OnMessage;
            bot.StartReceivingAsync();
            bot.WaitForShutdown();
        }

        private static async void Bot_OnMessage(MessageEventArgs e)
        {
            Console.WriteLine($"{e.Message.Sender}: {e.Message.Text}");

            if (e.Message.Sender != UserId)
            {
                QuickReply qr = null;

                if (e.Message.Text == "q")
                {
                    qr = new QuickReply();
                    qr.Options.Add(new QuickReplyOption() { Label = "1", Metadata = "#1#" });
                    qr.Options.Add(new QuickReplyOption() { Label = "2", Metadata = "#2#" });
                    qr.Options.Add(new QuickReplyOption() { Label = "3", Metadata = "#3#" });
                }

                await bot.SendMessageAsync(e.Message.Sender, e.Message.Text, qr);
            }
        }
    }
}
```

# ![GitHub Logo](https://github.com/4egod/Messenger.Bot/raw/master/Messenger.Small.png) Bots.Messenger 

Bots.Messenger is a .NET implementation of Facebook Messenger Platform which is a toolbox for building bots.

### How to use:

```csharp
    using Messenger.Bot;

    class Program
    {
        static MessengerBot bot = new MessengerBot(AppSecret, PageToken, VerifyToken);

        static void Main(string[] args)
        {
            Console.WriteLine("Starting the bot...");

            SetupBotProfile();

            bot.MessageReceived += Bot_MessageReceived;
            bot.StartReceivingAsync();

            SendMessages();

            bot.WaitForShutdown();
        }

        static async void SetupBotProfile()
        {
            await bot.SetStartButtonPostback("#get_started_button_postback#");
        }

        static async void SendMessages()
        {
            await bot.SendMessageAsync(UserId, "Test message");
        }

        static void Bot_MessageReceived(MessageEventArgs e)
        {
            Console.WriteLine($"\nMESSAGE:{e.Sender}:{e.Message.Text}");

            if (e.Message.QuickReply != null)
            {
                Console.WriteLine($"QUICK_REPLY:{e.Message.QuickReply.Payload}");
            }
        }
    }
```