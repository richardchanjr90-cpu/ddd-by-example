using System;
using System.Threading.Tasks;
using AzureFunctions.Extensions.NotificationHubs.Output;
using Loyalty.Application.Storage.Dto;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using static AzureFunctions.Extensions.NotificationHubs.Enum.Platform;

namespace LoyaltyProgram.Storage
{
    public static class PurchaseNotificationFunction
    {
        [FunctionName("PurchaseNotificationFunction")]
        public static async Task Run(
            [QueueTrigger("purchase-notification", Connection = "QueueConnectionString")] PurchaseNotificationDto data,
            ILogger log,
            [NotificationHubs] IAsyncCollector<HubsMessage> output)
        {
            log.LogInformation($"{nameof(PurchaseNotificationFunction)} was triggered.");

            if (!String.IsNullOrEmpty(data.Message) && !String.IsNullOrEmpty(data.UserId))
            {
                await output.AddAsync(new HubsMessage(data.Message, Android, "platform:android", $"user:{data.UserId}"));
                await output.AddAsync(new HubsMessage(data.Message, Apple, "platform:apple", $"user:{data.UserId}"));
            }
        }
    }
}
