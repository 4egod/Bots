using Bots;
using Bots.Twitter;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

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

            bot.OnMessage += Bot_OnMessage;
            bot.OnFollow += Bot_OnFollow;
            bot.OnUnFollow += Bot_OnUnFollow;
            bot.OnTweet += Bot_OnTweet; 
            bot.OnRetweet += Bot_OnRetweet;
            bot.OnQuote += Bot_OnQuote;
            bot.OnComment += Bot_OnComment;
            bot.OnMention += Bot_OnMention;
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

        private static async void Bot_PostReceived(WebhookEventArgs e)
        {
            if (!e.IsValid) return;

            await Task.Delay(50);

            if (RawDebug)
            {
                object o = JsonConvert.DeserializeObject(e.Body);
                string s = JsonConvert.SerializeObject(o, Formatting.Indented);
                Console.WriteLine("\n" + s);
            }
        }

        private static void Bot_OnMessage(MessageEventArgs e)
        {
            WriteLine(e.Recipient, "Message", $"[{e.Message.Sender}] => [{e.Message.Text}]");
        }

        private static void Bot_OnFollow(FollowEventArgs e)
        {
            WriteLine(e.Recipient, "Follow", $"[{e.Source.Id}:{e.Source.ScreenName}] => [{e.Target.Id}:{e.Target.ScreenName}]");
        }

        private static void Bot_OnUnFollow(FollowEventArgs e)
        {
            WriteLine(e.Recipient, "Unfollow", $"[{e.Source.Id}:{e.Source.ScreenName}] => [{e.Target.Id}:{e.Target.ScreenName}]");
        }

        private static void Bot_OnTweet(TweetEventArgs e)
        {
            WriteLine(e.Recipient, "Tweet", $"[{e.Tweet.Creator.Id}:{e.Tweet.Creator.ScreenName}] => [{e.Tweet.Id}:{e.Tweet.Text}]");
        }

        private static void Bot_OnRetweet(TweetEventArgs e)
        {
            WriteLine(e.Recipient, "Retweet", $"[{e.Tweet.Creator.Id}:{e.Tweet.Creator.ScreenName}] => " +
                $"[{e.Tweet.RetweetedFrom.Id}:{e.Tweet.RetweetedFrom.Text}] => [{e.Tweet.Id}]");
        }

        private static void Bot_OnQuote(TweetEventArgs e)
        {
            WriteLine(e.Recipient, "Quote", $"[{e.Tweet.Creator.Id}:{e.Tweet.Creator.ScreenName}] => " +
                 $"[{e.Tweet.QuotedFrom.Id}:{e.Tweet.QuotedFrom.Text}] => [{e.Tweet.Id}:{e.Tweet.Text}]");
        }

        private static void Bot_OnComment(TweetEventArgs e)
        {
            WriteLine(e.Recipient, "Comment", $"[{e.Tweet.Creator.Id}:{e.Tweet.Creator.ScreenName}] => " +
                $"[{e.Tweet.ReplyToStatusId.Value}] => [{e.Tweet.Id}:{e.Tweet.Text}]");
        }

        private static void Bot_OnMention(TweetEventArgs e)
        {
            WriteLine(e.Recipient, "Mention", $"[{e.Tweet.Creator.Id}:{e.Tweet.Creator.ScreenName}] => " +
                $"[{e.Tweet.ReplyToUser}] => [{e.Tweet.Id}:{e.Tweet.Text}]");
        }

        private static void Bot_OnLike(LikeEventArgs e)
        {
            WriteLine(e.Recipient, "Like", $"[{e.User.Id}:{e.User.ScreenName}] => [{e.Tweet.IdAsString}:{e.Tweet.Text}]");
        }

        private static void WriteLine(long recipient, string what, string message)
        {
            Console.WriteLine($"[Recipient:{recipient.ToString().PadRight(20)}] [{what.PadRight(10)}] {message}");
        }
    }
}
