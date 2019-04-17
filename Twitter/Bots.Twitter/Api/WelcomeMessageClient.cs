using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Bots.Twitter.Api
{
    using Models;
    using Converters;

    internal class WelcomeMessageRaw
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("created_timestamp")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("message_data")]
        public MessageData Data { get; set; }

        public WelcomeMessage ToWelcomeMessage()
        {
            WelcomeMessage m = new WelcomeMessage
            {
                Id = Id,
                Timestamp = Timestamp,
                Name = Name,
                Text = Data.Text,
                QuickReply = Data.QuickReply,
            };

            return m;
        }
    }

    internal class WelcomeMessageContainerRaw
    {
        [JsonProperty("welcome_message")]
        public WelcomeMessageRaw Data { get; set; }
    }

    internal class WelcomeRuleRaw
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("welcome_message_id")]
        public long MessageId { get; set; }

        [JsonProperty("created_timestamp")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }

    internal class WelcomeRuleContainerRaw
    {
        [JsonProperty("welcome_message_rule")]
        public WelcomeRuleRaw Data { get; set; }
    }

    internal class WelcomeMessageClient : BaseApiClient
    {
        public WelcomeMessageClient(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret) :
            base(consumerKey, consumerSecret, accessToken, accessTokenSecret)
        {
        }

        public override string ApiUri => base.ApiUri + "direct_messages/welcome_messages/";

        public async Task<WelcomeMessageRaw> CreateWelcomeMessageAsync(string name, string text, QuickReply quickReply)
        {
            WelcomeMessageRaw message = new WelcomeMessageRaw()
            {
                Name = name,
                Data = new MessageData()
                {
                    Text = text,
                    QuickReply = quickReply
                }
            };

            WelcomeMessageContainerRaw container = new WelcomeMessageContainerRaw()
            {
                Data = message
            };

            return (await PostAsync<WelcomeMessageContainerRaw>(container, ApiUri + "new.json")).Data;
        }

        public async Task<bool> DeleteWelcomeMessageAsync(long messageId)
        {
            var res = await DeleteAsync(ApiUri + $"destroy.json?id={messageId}");

            return res == System.Net.HttpStatusCode.NoContent;
        }

        public async Task<WelcomeRuleRaw> CreateWelcomeRuleAsync(long messageId)
        {
            WelcomeRuleRaw rule = new WelcomeRuleRaw()
            {
                MessageId = messageId
            };

            WelcomeRuleContainerRaw container = new WelcomeRuleContainerRaw()
            {
                Data = rule
            };

            return (await PostAsync<WelcomeRuleContainerRaw>(container, ApiUri + "rules/new.json")).Data;
        }

        public async Task<bool> DeleteWelcomeRuleAsync(long ruleId)
        {
            var res = await DeleteAsync(ApiUri + $"rules/destroy.json?id={ruleId}");

            return res == System.Net.HttpStatusCode.NoContent;
        }
    }
}
