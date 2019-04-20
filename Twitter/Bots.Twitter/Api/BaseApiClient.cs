using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bots.Twitter.Api
{
    using Models;
    using System.Net;

    internal abstract class BaseApiClient
    {
        public readonly Version ApiVersion = new Version(1, 1);

        private HttpClient httpClient;

        public BaseApiClient(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            AccessToken = accessToken;
            AccessTokenSecret = accessTokenSecret;

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentTypes.ApplicationJson));
        }

        public virtual string ApiUri => $"https://api.twitter.com/{ApiVersion}/";

        public string ConsumerKey { get; private set; }

        public string ConsumerSecret { get; private set; }

        public string AccessToken { get; private set; }

        public string AccessTokenSecret { get; private set; }

        protected async Task<T> GetAsync<T>(string uri)
        {
            string response;
            using (HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                req.Headers.Authorization = GetAuthorizationHeader(uri, HttpMethod.Get);
                response = await (await httpClient.SendAsync(req)).Content.ReadAsStringAsync();
            }

            return Parse<T>(response);
        }

        protected async Task<T> PostAsync<T>(object request, string uri)
        {
            string response;

            using (HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                req.Headers.Authorization = GetAuthorizationHeader(uri, HttpMethod.Post);
                if (request != null)
                {
#if DEBUG
                    string requestCmd = JsonConvert.SerializeObject(request, Formatting.Indented);
#else
                    string requestCmd = JsonConvert.SerializeObject(request);
#endif

                    req.Content = new StringContent(requestCmd, Encoding.UTF8, ContentTypes.ApplicationJson);
                }
                
                response = await (await httpClient.SendAsync(req)).Content.ReadAsStringAsync();
            }

            return Parse<T>(response);
        }

        protected async Task<HttpStatusCode> DeleteAsync(string uri)
        {
            HttpStatusCode code;
            using (HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Delete, uri))
            {
                req.Headers.Authorization = GetAuthorizationHeader(uri, HttpMethod.Delete);
                var res = await httpClient.SendAsync(req);
                code = res.StatusCode;
            }

            return code;
        }

        private T Parse<T>(string response)
        {
            var errorContainer = JsonConvert.DeserializeObject<ApiErrorContainer>(response);
            if (errorContainer.Errors != null)
            {
                if (errorContainer.Errors.Count == 1)
                {
                    throw new ApiException(errorContainer.Errors[0]);
                }
                else
                {
                    throw new ApiException(response);
                }
            }

            T res = JsonConvert.DeserializeObject<T>(response);

            return res;
        }

        protected AuthenticationHeaderValue GetAuthorizationHeader(string uri, HttpMethod method)
        {
            string nonce = DateTime.Now.Ticks.ToString();
            string timestamp = DateTime.Now.ToUnixTimestamp().ToString();

            List<string> authParams = new List<string>();
            authParams.Add("oauth_consumer_key=" + ConsumerKey);
            authParams.Add("oauth_nonce=" + nonce);
            authParams.Add("oauth_signature_method=HMAC-SHA1");
            authParams.Add("oauth_timestamp=" + timestamp);
            authParams.Add("oauth_token=" + AccessToken);
            authParams.Add("oauth_version=1.0");

            SplitUri(uri, out string url, out string[] queryParams);

            List<string> requestParams = new List<string>(authParams);
            requestParams.AddRange(queryParams);
            requestParams.Sort();

            string signatureBaseString = string.Join("&", requestParams);
            signatureBaseString = string.Concat(method.Method.ToUpper(), "&", Uri.EscapeDataString(url), "&", Uri.EscapeDataString(signatureBaseString));

            string signature = GetSignature(signatureBaseString);

            string hv = string.Join(", ", authParams.Select(x => x.Replace("=", " = \"") + '"'));
            hv += $", oauth_signature=\"{Uri.EscapeDataString(signature)}\"";      

            return new AuthenticationHeaderValue("OAuth", hv);
        }

        protected string GetSignature(string signatureBaseString)
        {
            byte[] key = Encoding.ASCII.GetBytes(string.Concat(Uri.EscapeDataString(ConsumerSecret), "&", Uri.EscapeDataString(AccessTokenSecret)));

            string signature;
            using (HMACSHA1 hasher = new HMACSHA1(key))
            {
                signature = Convert.ToBase64String(hasher.ComputeHash(Encoding.ASCII.GetBytes(signatureBaseString)));
            }

            return signature;
        }

        protected void SplitUri(string uri, out string url, out string[] queryParams)
        {
            int pos = uri.IndexOf('?');
            url = uri.Substring(0, pos);
            queryParams = uri.Substring(pos + 1).Split('&');
        }
    }
}
