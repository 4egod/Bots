
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bots.Twitter.Api
{
    internal class TweetClient : BaseApiClient
    {
        public TweetClient(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret) : 
            base(consumerKey, consumerSecret, accessToken, accessTokenSecret)
        {
        }

        public override string ApiUri => base.ApiUri + "statuses/";

        public async Task<Tweet> GetTweetAsync(long tweetId)
        {
            return await GetAsync<Tweet>(ApiUri + $"show.json?id={tweetId}");
        }

        public async Task<Tweet> TweetAsync(string text)
        {
            return await PostAsync<Tweet>(null, ApiUri + $"update.json?status={Uri.EscapeDataString(text)}");
        }

        public async Task<Tweet> QuoteAsync(long tweetId, string text)
        {
            Tweet tweet = await GetTweetAsync(tweetId);

            return await PostAsync<Tweet>(null, ApiUri + $"update.json?status=" +
                $"{Uri.EscapeDataString(text + $" https://twitter.com/{tweet.Creator.ScreenName}/status/{tweetId}")}");
        }

        public async Task<Tweet> CommentAsync(long tweetId, string text)
        {
            Tweet tweet = await GetTweetAsync(tweetId);

            return await PostAsync<Tweet>(null, ApiUri + $"update.json?status={Uri.EscapeDataString(text + " @4egod")}" +
                $"&in_reply_to_status_id={tweetId}");
        }
    }
}
