using System;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace LoyaltyProgram.Http.Invite
{
    public static class TwilioInvite
    {
        [FunctionName("TwilioInvite")]
        [return: TwilioSms(
            AccountSidSetting = "AccountSidSetting",
            AuthTokenSetting = "AuthTokenSetting",
            From = "+13479605960")]
        public static CreateMessageOptions Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venues/{id}/invite")] HttpRequest req,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(TwilioInvite)} was triggered.");

            string who = req.Query["who"];
            string to = "+" + req.Query["to"];
            string name = req.Query["name"];
            int code = 1488;

            var phoneNumber = new PhoneNumber(to);

            return new CreateMessageOptions(phoneNumber)
            {
                MessagingServiceSid = "MG829cf1e996e60d1c77fa66eee2eb1500",
                Body = $"Ваш код: {code}\n" +
                       $"{who} пригласил вас в {name}. Код действителен 30 минут."
            };
        }
    }
}
