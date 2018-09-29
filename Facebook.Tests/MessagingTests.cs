using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facebook.Tests
{
    using Messaging;
    using System.Diagnostics;
    using static Consts;

    [TestClass]
    public class MessagingTests
    {
        private MessagingClient client;

        [TestInitialize]
        public void Intitialize()
        {
            client = new MessagingClient(PageToken);
        }

        [TestMethod]
        public void ApiUriTest()
        {
            string result = client.ApiUri;
            Trace.WriteLine($"ApiUri: {result}");
        }

        [TestMethod]
        public void SendMessageTest()
        {
            string result = client.SendMessageAsync(UserId, "Test message").Result;
            Trace.WriteLine($"Message Id: {result}");
        }

        [TestMethod]
        public void SendMessageWithQuickReplyTest()
        {
            List<QuickReply> qrl = new List<QuickReply>();
            qrl.Add(new QuickReply()
            {
                Title = "title 1",
                Payload = "callback#1",
                QuickReplyType = QuickReplyTypes.Text
            });

            qrl.Add(new QuickReply()
            {
                Title = "title 2",
                Payload = "callback#2",
                QuickReplyType = QuickReplyTypes.Text
            });

            string result = client.SendMessageAsync(1703132266465872, "some text", qrl).Result;
            Trace.WriteLine($"Message Id: {result}");
        }
        
        [TestMethod]
        public void RequestUserLocationTest()
        {
            string result = client.RequestUserLocationAsync(UserId, "Your location:").Result;
            Trace.WriteLine($"Message Id: {result}");
        }

        [TestMethod]
        public void RequestUserPhoneTest()
        {
            string result = client.RequestUserPhoneNumberAsync(UserId, "Your Phone Number:").Result;
            Trace.WriteLine($"Message Id: {result}");
        }

        [TestMethod]
        public void RequestUserEmailTest()
        {
            string result = client.RequestUserEmailAsync(UserId, "Your Email:").Result;
            Trace.WriteLine($"Message Id: {result}");
        }
    }
}
