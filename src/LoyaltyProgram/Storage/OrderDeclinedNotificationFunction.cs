using System;
using System.Threading.Tasks;
using AzureFunctions.Extensions.NotificationHubs.Output;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Storage.Dto.Orders;
using Loyalty.Shared.Contracts.Enums;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using static AzureFunctions.Extensions.NotificationHubs.Enum.Platform;

namespace LoyaltyProgram.Storage
{
    public static class OrderDeclinedNotificationFunction
    {
        [FunctionName("OrderDeclinedNotificationFunction")]
        public static async Task Run(
            [QueueTrigger("neworder-notification", Connection = "QueueConnectionString")] OrderDeclinedDto data,
            ILogger log,
            [NotificationHubs] IAsyncCollector<HubsMessage> output)
        {
            log.LogInformation($"{nameof(OrderDeclinedNotificationFunction)} was triggered.");

            string message = String.Empty;

            if (data.UpdatedStatus == OrderStatus.DeclinedByCustomer
            || data.UpdatedStatus == OrderStatus.ForceDeclinedByCustomer)
            {
                message = "Клиент отменил свой заказ.";
            }

            if (data.VenueId > 0 && !String.IsNullOrEmpty(message))
            {
                await output.AddAsync(new HubsMessage(message, Android, "platform:android", $"venueId:{data.VenueId}"));
                await output.AddAsync(new HubsMessage(message, Apple, "platform:apple", $"venueId:{data.VenueId}"));
            }
        }
    }
}
