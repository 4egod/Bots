using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Messenger.Tests
{
    using Menu;
    using ProfileAPI;
    using static Consts;

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

        [TestMethod]
        public void SetPersistentMenuTest()
        {
            LocalizedMenuItem lmi = new LocalizedMenuItem();
            lmi.Locale = LocalizedMenuItem.DefaultLocale;
            lmi.Items = new List<IMenuItem>();

            NestedMenuItem nested = new NestedMenuItem()
            {
                Title = "My Account",
                Items = new List<IMenuItem>()
            };

            nested.Items.Add(new PostbackMenuItem() { Title = "Pay Bill", Payload = "PAYBILL_PAYLOAD" });
            nested.Items.Add(new UrlMenuItem() { Title = "Latest News", Url = "https://www.messenger.com/" });

            lmi.Items.Add(nested);

            PersistentMenu menu = new PersistentMenu();
            menu.Items = new List<LocalizedMenuItem>();
            menu.Items.Add(lmi);

            bool result = client.SetPersistentMenu(menu).Result;
            Trace.WriteLine(("Success: {0}".Format(result)));
        }
    }
}
