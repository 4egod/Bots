using System.Threading.Tasks;

namespace Bots.Twitter.Api
{
    internal class UsersClient : BaseApiClient
    {
        public UsersClient(string consumerKey, string consumerSeceret, string accessToken, string accessTokenSecret) :
            base(consumerKey, consumerSeceret, accessToken, accessTokenSecret)
        {
        }

        public override string ApiUri => base.ApiUri + "users/";

        public async Task<User> GetUserAsync(long userId)
        {
            return (await GetAsync<User>(ApiUri + $"show.json?user_id={userId}"));
        }

        public async Task<User> GetUserAsync(string screenName)
        {
            return (await GetAsync<User>(ApiUri + $"show.json?screen_name={screenName}"));
        }
    }
}
