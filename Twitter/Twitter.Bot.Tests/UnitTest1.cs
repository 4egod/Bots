using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Twitter.Bot.Tests
{
    using Twitter.Bot.Webhook;
    using Twitter.Bot;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            long l = 1554380290616;

            var dt = l.FromTwitterTimestamp();
        }
    }
}
