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

        private HttpClient httpClient;

        public MessagingClient(string pageToken)
        {
            PageToken = pageToken;
            httpClient = new HttpClient();
        }

        public string SendApiUri => $"https://graph.facebook.com/v{ApiVersion}/me/messages?access_token={PageToken}";

        public string MessengerProfileApiUri => $"https://graph.facebook.com/v{ApiVersion}/me/messenger_profile?access_token={PageToken}";

        public string PageToken { get; private set; }

        public async Task<string> SendMessageAsync(ulong userId, string text)
        {
            return await SendMessageAsync(userId, text, null);
        }

        public async Task<string> SendMessageAsync(ulong userId, string text, List<QuickReply> quickReplies)
        {
            if (text == null)
            {
                throw new NullReferenceException(nameof(text));
            }

            Recipient recipient = new Recipient() { id = userId.ToString() };
            Message message = new Message() { text = text };
            message.quick_replies = quickReplies;
            MessageContainer container = new MessageContainer()
            {
                recipient = recipient,
                message = message
            };

            var response = await PostAsync<Response>(container, SendApiUri);
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

        //public 

        public async Task<string> SendAttachment<T>(ulong userId, T attachment)
        {
            Recipient recipient = new Recipient() { id = userId.ToString() };
            Message<T> message = new Message<T>() { Attachment = attachment };
            MessageContainer container = new MessageContainer()
            {
                recipient = recipient,
                message = message
            };

            var response = await PostAsync<Response>(container, SendApiUri);
            return response.message_id;
        }

        protected async Task<T> PostAsync<T>(object request, string uri)
        {
#if DEBUG
            string requestCmd = JsonConvert.SerializeObject(request, Formatting.Indented);
#else
            string requestCmd = JsonConvert.SerializeObject(request);
#endif

            string response;

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                req.Content = new StringContent(requestCmd, Encoding.UTF8, "application/json");
                response = await (await httpClient.SendAsync(req)).Content.ReadAsStringAsync();
            }

            var errorContainer = JsonConvert.DeserializeObject<ApiErrorContainer>(response);
            if (errorContainer.Error != null)
            {
                throw new FacebookException(errorContainer.Error);
            }

            T res = JsonConvert.DeserializeObject<T>(response);

            return res;
        }
    }
}
