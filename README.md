# Messenger Platform .NET

[![NuGet](https://img.shields.io/nuget/v/Messenger.Platform.svg)](https://www.nuget.org/packages/Messenger.Platform)
![NuGet](https://img.shields.io/nuget/dt/Messenger.Platform.svg)

### How to use:

```csharp
MessengerBot bot = new MessengerBot(AppSecret, PageToken, VerifyToken);
bot.StartReceivingAsync();

var res = bot.SendMessageAsync(UserId, "Test message").Result;

bot.WaitForShutdown();
```