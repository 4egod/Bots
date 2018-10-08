# ![GitHub Logo](https://github.com/4egod/Messenger.Platform/raw/master/Messenger.Platform/Messenger.Small.png) Messenger Platform .NET

Messenger.Bot is a .NET implementation of Facebook Messenger Platform which is a toolbox for building bots.

[![NuGet](https://img.shields.io/nuget/v/Messenger.Platform.svg)](https://www.nuget.org/packages/Messenger.Platform)
[![NuGet](https://img.shields.io/nuget/dt/Messenger.Platform.svg)](https://www.nuget.org/packages/Messenger.Platform)

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
    }
```