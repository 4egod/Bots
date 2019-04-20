
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

        public async Task<Tweet> TweetAsync(string text)
        {
            return await PostAsync<Tweet>(null, ApiUri + $"update.json?status={Uri.EscapeDataString(text)}");
        }
    }
}
