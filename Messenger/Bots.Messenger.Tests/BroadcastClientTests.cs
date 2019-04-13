using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Messenger.Bot.Tests
{
    using BroadcastAPI;
    using static Consts;

    [TestClass]
    public class BroadcastClientTests
    {
        private BroadcastApiClient client;
        private BroadcastMessageResult message;
        private BroadcastResult broadcast;

        [TestInitialize]
        public void Intitialize()
        {
            client = new BroadcastApiClient(PageToken);
        }

        [TestMethod]
        public void CreateMessageTest()
        {
            var r = BroadcastMessageResult.CreateFailed<BroadcastMessageResult>();
            var result = client.CreateMessageAsync("Broadcasted message").Result;
            message = result;
            Trace.WriteLine($"Message Id: {result.Id}");
        }

        [TestMethod]
        public void CreateMessageWithQuickReplyTest()
        {
            List<QuickReply> qrl = new List<QuickReply>();
            qrl.Add(new QuickReply()
            {
                Title = "title 1",
                Payload = "postback#1",
                QuickReplyType = QuickReplyTypes.Text
            });

            qrl.Add(new QuickReply()
            {
                Title = "title 2",
                Payload = "postback#2",
                QuickReplyType = QuickReplyTypes.Text
            });

            var result = client.CreateMessageAsync("Broadcast message with quick replies", qrl).Result;
            Trace.WriteLine($"Message Id: {result.Id}");
        }

        [TestMethod]
        public void SendMessageTest()
        {
            CreateMessageTest();
            var result = client.BroadcastMessageAsync(message, NotificationTypes.Regular).Result;
            Trace.WriteLine($"Broadcast Id: {result.Id}");
        }

        [TestMethod]
        public void ScheduleMessageTest()
        {
            CreateMessageTest();
            var result = client.BroadcastMessageAsync(message, NotificationTypes.Regular, DateTime.UtcNow.AddMinutes(5)).Result;
            broadcast = result;
            Trace.WriteLine($"Broadcast Id: {result.Id}");
        }

        [TestMethod]
        public void CancelScheduledBroadcastTest()
        {
            ScheduleMessageTest();
            bool result = client.CancelScheduledBroadcast(broadcast).Result;
            Trace.WriteLine($"Canceled: {result}");
        }
    }
}
