using System;
using System.Net.Http;
using System.Threading.Tasks;
using Loyalty.Application.Storage.Dto;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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

            var message = $"{data.Inviter} пригласил вас в Zalik.App https://zalik.app/store";

            string token = "***REDACTED***";
            string phone = data.WorkerPhone.Replace("+", String.Empty);
            string alphanameId = "Zalik.App";

            var alphaUri = "http://app.sms.by/api/v1/getAlphanameId" +
                        $"?token={token}" +
                        $"&name={alphanameId}";

            var alphaResponse = await Client.GetAsync(new Uri(alphaUri));
            var alpha = await alphaResponse.Content.ReadAsStringAsync();
            dynamic alphaId = JsonConvert.DeserializeObject(alpha);

            var uri = "http://app.sms.by/api/v1/sendQuickSms?" +
                      $"token={token}&" +
                      $"message={message}" +
                      $"&phone={phone}" +
                      $"&alphaname_id={alphaId.id}";

            if (phone.StartsWith("375"))
            {
                //var result = await Client.GetAsync(new Uri(uri));
                var sendSmsResponse = await Client.GetAsync(new Uri(uri));
                var contents = await sendSmsResponse.Content.ReadAsStringAsync();
                log.LogDebug($"SMS to {phone} sent with {contents}");
            }
        }
    }
}
