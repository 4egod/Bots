using Bots.Twitter.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bots.Twitter.Tests
{
    using System.Diagnostics;
    using static Consts;

    [TestClass]
    public class DirectMessagesClientTests
    {
        private DirectMessagesClient client;

        [TestInitialize]
        public void Intitialize()
        {
            client = new DirectMessagesClient(ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret);
        }

        [TestMethod]
        public void ApiUriTest()
        {
            string result = client.ApiUri;
            Trace.WriteLine($"ApiUri: {result}");
        }

        [TestMethod]
        public void GetUserTest()
        {
            var result = client.SendDirectMessageAsync(UserId, "test_message").Result;
            Trace.WriteLine($"Message Id: {result.Id}");
        }
    }
}
