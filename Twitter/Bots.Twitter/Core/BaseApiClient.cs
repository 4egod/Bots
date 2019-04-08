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

namespace Bots.Twitter
{
    using Models;

    public abstract class BaseApiClient
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
                response = await (await httpClient.SendAsync(req)).Content.ReadAsStringAsync();
            }

            //var errorContainer = JsonConvert.DeserializeObject<ApiErrorContainer>(response);
            //if (errorContainer.Error != null)
            //{
            //    throw new ApiException(errorContainer.Error);
            //}

            T res = JsonConvert.DeserializeObject<T>(response);

            return res;
        }

        protected async Task<T> PostAsync<T>(object request, string uri)
        {
#if DEBUG
            string requestCmd = JsonConvert.SerializeObject(request, Formatting.Indented);
#else
            string requestCmd = JsonConvert.SerializeObject(request);
#endif
            string response;

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentTypes.ApplicationJson));

            using (HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                req.Headers.Add("Authorization", GenerateAuthorizationHeader(uri, HttpMethod.Post));
                req.Content = new StringContent(requestCmd, Encoding.UTF8, ContentTypes.ApplicationJson);
                response = await (await httpClient.SendAsync(req)).Content.ReadAsStringAsync();
            }

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

        protected string GenerateAuthorizationHeader(string requestUri, HttpMethod method)
        {
            Uri uri = new Uri(requestUri, UriKind.RelativeOrAbsolute);

            string nonce = DateTime.Now.Ticks.ToString();
            string timestamp = DateTime.Now.ToUnixTimestamp().ToString();

            Dictionary<string, string> authParams = new Dictionary<string, string>();
            authParams.Add("oauth_consumer_key", ConsumerKey);
            authParams.Add("oauth_nonce", nonce);
            authParams.Add("oauth_signature_method", "HMAC-SHA1");
            authParams.Add("oauth_timestamp", timestamp);
            authParams.Add("oauth_token", AccessToken);
            authParams.Add("oauth_version", "1.0");

            Dictionary<string, string> sigParams = new Dictionary<string, string>();
            foreach (var item in authParams)
            {
                sigParams.Add(item.Key, item.Value);
            }

            Dictionary<string, string> queryParams = GetParams(uri);
            foreach (var item in queryParams)
            {
                sigParams.Add(item.Key, item.Value);
            }

            string s = string.Join("&", sigParams.OrderBy(x => x.Key).Select(x => $"{x.Key}={x.Value}"));
            string url = requestUri.Contains("?") ? requestUri.Substring(0, requestUri.IndexOf("?")) : requestUri;

            s = string.Concat(method.Method.ToUpper(), "&", Uri.EscapeDataString(url), "&", Uri.EscapeDataString(s));

            byte[] key = Encoding.ASCII.GetBytes(string.Concat(Uri.EscapeDataString(ConsumerSecret), "&", Uri.EscapeDataString(AccessTokenSecret)));

            string signature;
            using (HMACSHA1 hasher = new HMACSHA1(key))
            {
                signature = Convert.ToBase64String(hasher.ComputeHash(Encoding.ASCII.GetBytes(s)));
            }

            string res = "OAuth ";
            foreach (var item in authParams)
            {
                res += $"{item.Key} = \"{Uri.EscapeDataString(item.Value)}\", ";
            }

            res += $"oauth_signature = \"{Uri.EscapeDataString(signature)}\"";

            return res;
        }

        private Dictionary<string, string> GetParams(Uri uri)
        {
            MatchCollection matches = Regex.Matches(uri.AbsoluteUri, @"[\?&](([^&=]+)=([^&=#]*))", RegexOptions.Compiled);
            Dictionary<string, string> keyValues = new Dictionary<string, string>(matches.Count);
            foreach (Match m in matches)
            {
                keyValues.Add(Uri.UnescapeDataString(m.Groups[2].Value), Uri.UnescapeDataString(m.Groups[3].Value));
            }

            return keyValues;
        }
    }
}
