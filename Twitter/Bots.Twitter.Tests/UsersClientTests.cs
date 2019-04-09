using Bots.Twitter.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bots.Twitter.Tests
{
    using System.Diagnostics;
    using static Consts;

    [TestClass]
    public class UsersClientTests
    {
        private UsersClient client;

        [TestInitialize]
        public void Intitialize()
        {
            client = new UsersClient(ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret);
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
            User result = client.GetUserAsync(UserId).Result;
            Trace.WriteLine($"User: {result.ToJson()}");
        }

        [TestMethod]
        public void GetUserByNameTest()
        {
            User result = client.GetUserAsync("4egod").Result;
            Trace.WriteLine($"User: {result.ToJson()}");
        }
    }
}
