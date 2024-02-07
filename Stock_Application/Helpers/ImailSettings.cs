using Microsoft.Extensions.Options;
using Twilio.Rest.Api.V2010.Account;

namespace Api.Helpers
{
    public class ImailSettings : Is
    {
        private readonly IOptions<Is> _options;
        public ImailSettings()
        {
            
        }

        //public MessageResource Send(SmsMessage sms)
    }
}
