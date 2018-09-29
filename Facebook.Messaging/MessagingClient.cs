using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Messaging
{
    public class MessagingClient
    {
        public readonly Version ApiVersion = new Version(3, 1);


        private string pageToken;
        private HttpClient httpClient;

        public MessagingClient(string pageToken)
        {
            this.pageToken = pageToken;
            httpClient = new HttpClient();
        }

        public string ApiUri => $"https://graph.facebook.com/v{ApiVersion}/me/messages?access_token={pageToken}";

        public async Task<string> SendMessageAsync(ulong userId, string text)
        {
            return await SendMessageAsync(userId, text, null);
        }

        public async Task<string> SendMessageAsync(ulong userId, string text, List<QuickReply> quickReplies)
        {
            Recipient recipient = new Recipient() { id = userId.ToString() };
            Message message = new Message() { text = text };
            message.quick_replies = quickReplies;
            MessageContainer container = new MessageContainer()
            {
                recipient = recipient,
                message = message
            };

            var response = await PostAsync<Response>(container, ApiUri);
            return response.message_id;
        }

        public async Task<string> RequestUserLocationAsync(ulong userId, string message)
        {
            List<QuickReply> qrl = new List<QuickReply>();
            qrl.Add(new QuickReply()
            {
                QuickReplyType = QuickReplyTypes.Location
            });

            return await SendMessageAsync(userId, message, qrl);
        }

        public async Task<string> RequestUserPhoneNumberAsync(ulong userId, string message)
        {
            List<QuickReply> qrl = new List<QuickReply>();
            qrl.Add(new QuickReply()
            {
                QuickReplyType = QuickReplyTypes.UserPhoneNumber
            });

            return await SendMessageAsync(userId, message, qrl);
        }

        public async Task<string> RequestUserEmailAsync(ulong userId, string message)
        {
            List<QuickReply> qrl = new List<QuickReply>();
            qrl.Add(new QuickReply()
            {
                QuickReplyType = QuickReplyTypes.UserEmail
            });

            return await SendMessageAsync(userId, message, qrl);
        }



        protected async Task<T> PostAsync<T>(object request, string uri)
        {
            string requestCmd = JsonConvert.SerializeObject(request);

            string response;

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                req.Content = new StringContent(requestCmd, Encoding.UTF8, "application/json");
                response = await (await httpClient.SendAsync(req)).Content.ReadAsStringAsync();
            }

            T res = JsonConvert.DeserializeObject<T>(response);

            return res;
        }
    }
}
