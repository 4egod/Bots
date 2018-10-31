using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messenger.Bot.Extension;
using Messenger.Bot.ProfileAPI;
using Newtonsoft.Json;

namespace Messenger.Bot.UserProfileAPI
{
    /// <summary>
    /// https://developers.facebook.com/docs/messenger-platform/identity/user-profile/
    /// </summary>
    public class UserProfileApiClient : BaseApiClient
    {
        public UserProfileApiClient(string pageToken) : base(pageToken)
        {
            
        }

        public override string ApiUri => $"https://graph.facebook.com/v{ApiVersion}/";

        public async Task<Dictionary<UserProfilePropertyTypes,string>> GetCustomerInfo(string userId,IList<UserProfilePropertyTypes> properties = null)
        {
            var url = ApiUri + $"{userId}?access_token={PageToken}";

            if (properties != null && properties.Any())
            {
                url = url + "&fields=" + string.Join(",", properties.Select(x=>x.ToEnumString()));
            }

            var result = await GetAsync<Dictionary<string,string>>(url);

            var returnDic = new Dictionary<UserProfilePropertyTypes, string>();
            foreach (var keyValue in result)
            {
                returnDic.Add(keyValue.Key.ToEnum<UserProfilePropertyTypes>(),keyValue.Value);
            }

            return returnDic;
        }
    }
}
