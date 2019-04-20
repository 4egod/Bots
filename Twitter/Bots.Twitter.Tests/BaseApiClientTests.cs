using Bots.Twitter.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bots.Twitter.Tests
{
    using System.Diagnostics;
    using System.Net.Http;
    using static Consts;

    [TestClass]
    public class BaseApiClientTests
    {
        internal class ApiClient : BaseApiClient
        {
            public ApiClient(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret) : base(consumerKey, consumerSecret, accessToken, accessTokenSecret)
            {
            }

            public override string ApiUri => "https://api.twitter.com/1.1/statuses/update.json?" +
                "status=test";
        }

        private ApiClient client;

        [TestInitialize]
        public void Intitialize()
        {
            client = new ApiClient(ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret);
        }

        //[TestMethod]
        //public void SplitUriTest()
        //{
        //    client.SplitUri(client.ApiUri, out string url, out string[] queryParams);
        //    Trace.WriteLine($"Url: {url}\n");

        //    Trace.WriteLine("Params:");
        //    foreach (var item in queryParams)
        //    {
        //        Trace.WriteLine($"{item}");
        //    }
        //}

        //[TestMethod]
        //public void GetSignatureBaseStringTest()
        //{
        //    var result = client.GetAuthorizationHeader(client.ApiUri, HttpMethod.Post);
        //}
    }
}
