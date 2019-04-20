using Bots.Twitter.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bots.Twitter.Tests
{
    using System;
    using System.Diagnostics;
    using static Consts;

    [TestClass]
    public class TweetClientTests
    {
        private TweetClient client;

        [TestInitialize]
        public void Intitialize()
        {
            client = new TweetClient(ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret);
        }

        [TestMethod]
        public void ApiUriTest()
        {
            string result = client.ApiUri;
            Trace.WriteLine($"ApiUri: {result}");
        }

        [TestMethod]
        public void GetUserByIdTest()
        {
            client.TweetAsync($"{DateTime.Now}").Wait();
        }
    }
}
