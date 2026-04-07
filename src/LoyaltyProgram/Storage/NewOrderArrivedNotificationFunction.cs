using System.Threading.Tasks;
using AzureFunctions.Extensions.NotificationHubs.Output;
using Loyalty.Application.Storage.Dto.Orders;
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

            const string message = "Поступил новый заказ.";

            if (data.VenueId > 0)
            {
                await output.AddAsync(new HubsMessage(message, Android, "platform:android", $"venueId:{data.VenueId}"));
                await output.AddAsync(new HubsMessage(message, Apple, "platform:apple", $"venueId:{data.VenueId}"));
            }
        }
    }
}
