using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Bot
{
    public abstract class BaseApiClient
    {
        public readonly Version ApiVersion = new Version(3, 1);

        private HttpClient httpClient;

        public BaseApiClient(string pageToken)
        {
            PageToken = pageToken;
            httpClient = new HttpClient();
        }

        public abstract string ApiUri { get; }

        public string PageToken { get; private set; }

        protected async Task<T> GetAsync<T>(string uri)
        {
            string response;
            using (HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                response = await (await httpClient.SendAsync(req)).Content.ReadAsStringAsync();
            }

            var errorContainer = JsonConvert.DeserializeObject<ApiErrorContainer>(response);
            if (errorContainer.Error != null)
            {
                throw new ApiException(errorContainer.Error);
            }

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
                req.Content = new StringContent(requestCmd, Encoding.UTF8, ContentTypes.ApplicationJson);
                response = await (await httpClient.SendAsync(req)).Content.ReadAsStringAsync();
            }

            var errorContainer = JsonConvert.DeserializeObject<ApiErrorContainer>(response);
            if (errorContainer.Error != null)
            {
                throw new ApiException(errorContainer.Error);
            }

            T res = JsonConvert.DeserializeObject<T>(response);

            return res;
        }
    }
}
