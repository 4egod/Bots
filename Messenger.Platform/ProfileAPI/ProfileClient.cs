using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.ProfileAPI
{
    using Menu;

    public class ProfileClient : BaseApiClient
    {
        public ProfileClient(string pageToken) : base(pageToken)
        {
        }

        public override string ApiUri => $"https://graph.facebook.com/v{ApiVersion}/me/messenger_profile?access_token={PageToken}";

        public async Task<bool> SetStartButtonPostback(string payload)
        {
            GetStartedContainer container = new GetStartedContainer()
            {
                Value = new GetStarted()
                {
                    Payload = payload
                }
            };

            OperationResult result = await PostAsync<OperationResult>(container, ApiUri);

            return result.Result;
        }

        public async Task<bool> SetGreeting(string text)
        {
            List<Greeting> values = new List<Greeting>();
            values.Add(new Greeting()
            {
                Locale = "default",
                Text = text
            });

            GreetingContainer container = new GreetingContainer()
            {
                Values = values
            };

            OperationResult result = await PostAsync<OperationResult>(container, ApiUri);

            return result.Result;
        }

        public async Task<bool> SetPersistentMenu(PersistentMenu menu)
        {
            OperationResult result = await PostAsync<OperationResult>(menu, ApiUri);

            return result.Result;
        }

        //private string BuildGetApiUri(FileldTypes fields)
        //{
        //    string fs = fields.ToString().Replace(" ", "");
        //    string api = "https://graph.facebook.com/v{0}/me/messenger_profile?fields={1}&access_token={2}";
        //    api = api.Format(ApiVersion, fs, PageToken);
        //    return api;
        //}
    }
}
