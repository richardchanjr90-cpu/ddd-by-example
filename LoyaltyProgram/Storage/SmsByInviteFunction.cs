using System;
using System.Net.Http;
using System.Threading.Tasks;
using Loyalty.Application.Storage.Dto;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Storage
{
    public static class SmsByInviteFunction
    {
        private static readonly HttpClient Client = new HttpClient();

        [FunctionName("SmsByInviteFunction")]
        public static async Task Run(
            [QueueTrigger("worker-invite", Connection = "QueueConnectionString")] WorkerInviteDto data,
            ILogger log)
        {
            log.LogInformation($"{nameof(SmsByInviteFunction)} was triggered.");

            var message = $"{data.Inviter} пригласил вас в Zalik.App Business " +
                       $"—сылка дл€ скачивани€: http://покаеенет.com";

            string token = "***REDACTED***";
            string phone = data.WorkerPhone;
            string alphanameId = "Zalik";

            var uri = "http://app.sms.by/api/v1/sendQuickSms?" +
                      $"token={token}&" +
                      $"message={message}" +
                      $"&phone={phone}" +
                      $"&alphaname_id={alphanameId}";

            if (phone.StartsWith("+375"))
            {
                await Client.GetAsync(new Uri(uri));
            }
        }
    }
}
