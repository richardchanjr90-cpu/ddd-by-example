using Loyalty.Application.Storage.Dto;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace LoyaltyProgram.Storage
{
    public static class TwilioInviteFunction
    {
        [FunctionName("TwilioInviteFunction")]
        [return: TwilioSms(
            AccountSidSetting = "AccountSidSetting",
            AuthTokenSetting = "AuthTokenSetting",
            From = "+13479605960")]
        public static CreateMessageOptions Run(
            [QueueTrigger("worker-invite", Connection = "QueueConnectionString")] WorkerInviteDto data,
            ILogger log)
        {
            log.LogInformation($"{nameof(TwilioInviteFunction)} was triggered.");
            var phoneNumber = new PhoneNumber(data.WorkerPhone);

            return new CreateMessageOptions(phoneNumber)
            {
                MessagingServiceSid = "MG829cf1e996e60d1c77fa66eee2eb1500",
                Body = $"{data.Inviter} пригласил вас в {data.VenueName}. " +
                       $"—сылка дл€ скачивани€: http://покаеенет.com"
            };
        }
    }
}
