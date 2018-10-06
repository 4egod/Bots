# Messenger Platform .NET

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