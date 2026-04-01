using System;
using System.Threading.Tasks;
using AzureFunctions.Extensions.NotificationHubs.Output;
using Loyalty.Application.Storage.Dto;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using static AzureFunctions.Extensions.NotificationHubs.Enum.Platform;

namespace LoyaltyProgram.Storage
{
    public static class NewOrderArrivedNotificationFunction
    {
        [FunctionName("NewOrderArrivedNotificationFunction")]
        public static async Task Run(
            [QueueTrigger("neworder-notification", Connection = "QueueConnectionString")] NewOrderDto data,
            ILogger log,
            [NotificationHubs] IAsyncCollector<HubsMessage> output)
        {
            log.LogInformation($"{nameof(NewOrderArrivedNotificationFunction)} was triggered.");

            if (!String.IsNullOrEmpty(data.Message) && data.VenueId > 0)
            {
                await output.AddAsync(new HubsMessage(data.Message, Android, "platform:android", $"venueId:{data.VenueId}"));
                await output.AddAsync(new HubsMessage(data.Message, Apple, "platform:apple", $"venueId:{data.VenueId}"));
            }
        }
    }
}
