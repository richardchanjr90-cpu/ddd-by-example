using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Loyalty.Application.Storage.Dto;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Common.Shared.Settings;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LoyaltyProgram.Storage
{
    public class SmsByInviteFunction
    {
        private static readonly HttpClient Client = new HttpClient();

        private readonly IOptions<SmsSettings> smsOptions;

        private readonly string getAlphaNameUri = "{0}/getAlphanameId" +
                                                  "?token={1}" +
                                                  "&name={2}";

        private readonly string sendSmsUri = "{0}/sendQuickSms?" +
                                                   "token={1}&" +
                                                   "message={2}" +
                                                   "&phone={3}" +
                                                   "&alphaname_id={4}";

        public SmsByInviteFunction(IOptions<SmsSettings> smsOptions)
        {
            this.smsOptions = smsOptions;
        }

        [FunctionName("SmsByInviteFunction")]
        public async Task Run(
            [QueueTrigger("worker-invite", Connection = "QueueConnectionString")] WorkerInviteDto data,
            ILogger log)
        {
            log.LogInformation($"{nameof(SmsByInviteFunction)} was triggered.");

            if (!String.IsNullOrEmpty(data.Inviter) && EnvironmentExtensions.IsProd())
            {
                var message = $"{data.Inviter} пригласил вас в Zalik Business https://zalik.app/store";

                var phone = data.WorkerPhone.Replace("+", String.Empty);

                var alphaUri = String.Format(
                    getAlphaNameUri, 
                    smsOptions.Value.Host, 
                    smsOptions.Value.Token, 
                    smsOptions.Value.AlphaNameId);

                var alphaResponse = await Client.GetAsync(new Uri(alphaUri));

                if (alphaResponse.IsSuccessStatusCode)
                {
                    var alpha = await alphaResponse.Content.ReadAsStringAsync();
                    var alphaId = (JsonElement)JsonSerializer.Deserialize<object>(alpha);

                    var uri = String.Format(
                        sendSmsUri, 
                        smsOptions.Value.Host, 
                        smsOptions.Value.Token,
                        message, 
                        phone, 
                        alphaId.GetProperty("id").ToString());

                    if (phone.StartsWith(smsOptions.Value.AllowedPrefix))
                    {
                        var sendSmsResponse = await Client.GetAsync(new Uri(uri));

                        if (sendSmsResponse.IsSuccessStatusCode)
                        {
                            var contents = await sendSmsResponse.Content.ReadAsStringAsync();
                            log.LogInformation($"SMS to {phone} sent with {@contents}", contents);
                        }
                        else
                        {
                            log.LogError("Failed to deliver message with error: {@sendSmsResponse}", sendSmsResponse.Content, sendSmsResponse);
                        }
                    }
                }
                else
                {
                    log.LogError("Failed to get Alpha URI with error: {@AlphaResponse}", alphaResponse.StatusCode, alphaResponse);
                }
            }
        }
    }
}
