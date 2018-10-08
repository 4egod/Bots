# ![GitHub Logo](https://github.com/4egod/Messenger.Bot/raw/master/Messenger.Small.png) Messenger Platform .NET

Messenger.Bot is a .NET implementation of Facebook Messenger Platform which is a toolbox for building bots.

[![NuGet](https://img.shields.io/nuget/v/Messenger.Bot.svg)](https://www.nuget.org/packages/Messenger.Bot)
[![NuGet](https://img.shields.io/nuget/dt/Messenger.Bot.svg)](https://www.nuget.org/packages/Messenger.Bot)

### How to use:

```csharp
    using Messenger;

    class Program
    {
        static MessengerBot bot = new MessengerBot(AppSecret, PageToken, VerifyToken);

        static void Main(string[] args)
        {
            Console.WriteLine("Starting the bot...");

            SetupBotProfile();

            bot.OnMessage += Bot_OnMessage;
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

        static void Bot_OnMessage(MessageEventArgs e)
        {
            Console.WriteLine($"\nMESSAGE:{e.Sender}:{e.Message.Text}");

            if (e.Message.QuickReply != null)
            {
                Console.WriteLine($"QUICK_REPLY:{e.Message.QuickReply.Payload}");
            }
        }
    }
```