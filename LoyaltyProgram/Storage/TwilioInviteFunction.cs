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
                Body = $"{data.Inviter} пригласил вас в Zalik.App Business " +
                       $"—сылка дл€ скачивани€: http://покаеенет.com"
            };
        }
    }
}
