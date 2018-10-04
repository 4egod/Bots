using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Messenger.Tests
{
    using static Consts;
    using ProfileAPI;
    using System.Diagnostics;
    using System;
    using System.Globalization;

    [TestClass]
    public class ProfileClientTests
    {
        private ProfileClient client;

        [TestInitialize]
        public void Intitialize()
        {
            client = new ProfileClient(PageToken);
        }

        [TestMethod]
        public void SetStartButtonPostbackTest()
        {
            bool result = client.SetStartButtonPostback("#start_postback#"+DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss")).Result;
            Trace.WriteLine(("Success: {0}".Format(result)));
        }

        [TestMethod]
        public void SetGreetingTest()
        {
            bool result = client.SetGreeting("Custom Welocome String!").Result;
            Trace.WriteLine(("Success: {0}".Format(result)));
        }
    }
}
