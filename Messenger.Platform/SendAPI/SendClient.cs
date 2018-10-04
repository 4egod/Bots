using System;
using System.Collections.Generic;
using System.Text;

namespace Messenger.SendAPI
{
    public class SendClient : BaseApiClient
    {
        public SendClient(string pageToken) : base(pageToken)
        {
        }

        public override string ApiUri => $"https://graph.facebook.com/v{ApiVersion}/me/messenger_profile?access_token={PageToken}";
    }
}
