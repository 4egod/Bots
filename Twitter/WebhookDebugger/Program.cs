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

        static bool RawDebug = true;

        static void Main(string[] args)
        {
            if (RawDebug)
            {
                bot.PostReceived += Bot_PostReceived;            
            }
            bot.InvalidPostReceived += Bot_InvalidPostReceived;

            bot.OnMessage += Bot_OnMessage;
            bot.OnFollow += Bot_OnFollow;
            bot.OnTweet += Bot_OnTweet; 

            bot.OnRetweet += Bot_OnRetweet;
            bot.OnQuote += Bot_OnQuote;
            bot.OnComment += Bot_OnComment;
            bot.OnLike += Bot_OnLike;
            bot.StartReceivingAsync();

            while (true)
            {
                Console.ReadLine();
                Console.Clear();
            }
        }

        private static async void Bot_InvalidPostReceived(WebhookEventArgs e)
        {
            await Task.Delay(50);
            Console.WriteLine("Invalid POST received (invalid or empty signature)");
        }

        private static void Bot_PostReceived(WebhookEventArgs e)
        {
            if (!e.IsValid) return;

            if (RawDebug)
            {
                object o = JsonConvert.DeserializeObject(e.Body);
                string s = JsonConvert.SerializeObject(o, Formatting.Indented);
                Console.WriteLine("\n" + s);
            }
        }

        private static void Bot_OnMessage(MessageEventArgs e)
        {
            Console.WriteLine($"[Message] {e.Message.Sender}: {e.Message.Text}");
        }

        private static void Bot_OnFollow(FollowEventArgs e)
        {
            Console.WriteLine($"[Follow] User {e.Source.ScreenName} ({e.Source.Id}) followed " +
                $"{e.Target.ScreenName} ({e.Target.Id})");
        }

        private static void Bot_OnTweet(TweetEventArgs e)
        {
            Console.WriteLine($"[Tweet] Author: {e.Tweet.Creator} tweeted {e.Tweet.Text} ({e.Tweet.Id})");
        }

        private static void Bot_OnRetweet(TweetEventArgs e)
        {
            Console.WriteLine($"[Retweet] User {e.Tweet.Creator.ScreenName} ({e.Tweet.Creator.IdAsString}) retweet " +
                $"{e.Tweet.RetweetedFrom.Text} ({e.Tweet.RetweetedFrom.IdAsString}) => Retweet Id: {e.Tweet.IdAsString}");
        }

        private static void Bot_OnQuote(TweetEventArgs e)
        {
            Console.WriteLine($"[Quote] User {e.Tweet.Creator.ScreenName} ({e.Tweet.Creator.IdAsString}) quoted tweet " +
                $"{e.Tweet.QuotedFrom.Text} ({e.Tweet.QuotedFrom.IdAsString}) => {e.Tweet.Text} ({e.Tweet.IdAsString})");
        }

        private static void Bot_OnComment(TweetEventArgs e)
        {
            Console.WriteLine($"[Comment] User {e.Tweet.Creator.ScreenName} ({e.Tweet.Creator.IdAsString}) comment tweet " +
                $"{e.Tweet.ReplyToStatusId.Value} => {e.Tweet.Text} ({e.Tweet.IdAsString})");
        }

        private static void Bot_OnLike(LikeEventArgs e)
        {
            Console.WriteLine($"[Like] User {e.User.ScreenName} ({e.User.IdAsString}) liked {e.Tweet.Text} ({e.Tweet.IdAsString})");
        }
    }
}
