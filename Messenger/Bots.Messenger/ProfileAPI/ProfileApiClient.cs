using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messenger.Bot.ProfileAPI
{
    using Menu;

    public class ProfileApiClient : BaseApiClient
    {
        public ProfileApiClient(string pageToken) : base(pageToken)
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
    }
}
