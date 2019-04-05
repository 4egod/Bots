using System;
using System.Threading.Tasks;

namespace Twitter.Bot.DirectMessagesAPI
{
    using Models;

    internal class DirectMessagesApiClient : BaseApiClient
    {
        public DirectMessagesApiClient(string consumerKey, string consumerSeceret, string accessToken, string accessTokenSecret) :
            base(consumerKey, consumerSeceret, accessToken, accessTokenSecret)
        {
        }

        public override string ApiUri => $"https://api.twitter.com/{ApiVersion}/direct_messages/";

        public async Task<MessageCreateEvent> SendDirectMessageAsync(long userId, string text)
        {
            return await SendDirectMessageAsync(userId, text, null);
        }

        public async Task<MessageCreateEvent> SendDirectMessageAsync(long userId, string text, QuickReply quickReply)
        {
            MessageCreateEvent messageCreateEvent = new MessageCreateEvent()
            {
                Timestamp = DateTime.UtcNow,
                Data = new MessageCreateData()
                {
                    Target = new Target()
                    {
                        RecipientId = userId
                    },

                    Data = new MessageData()
                    {
                        Text = text,
                        QuickReply = quickReply
                    }
                }
            };

            Event<MessageCreateEvent> eventContainer = new Event<MessageCreateEvent>()
            {
                Data = messageCreateEvent
            };

            return (await PostAsync<Event<MessageCreateEvent>>(eventContainer, ApiUri + "events/new.json")).Data;
        }

    }
}
