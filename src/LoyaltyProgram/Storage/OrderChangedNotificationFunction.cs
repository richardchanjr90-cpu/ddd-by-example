using System;
using System.Threading.Tasks;
using AzureFunctions.Extensions.NotificationHubs.Output;
using Loyalty.Application.Storage.Dto.Orders;
using Loyalty.Shared.Contracts.Enums;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using static AzureFunctions.Extensions.NotificationHubs.Enum.Platform;

namespace LoyaltyProgram.Storage
{
    public static class OrderChangedNotificationFunction
    {
        [FunctionName("OrderChangedNotificationFunction")]
        public static async Task Run(
            [QueueTrigger("changedorder-notification", Connection = "QueueConnectionString")] OrderChangedDto data,
            ILogger log,
            [NotificationHubs] IAsyncCollector<HubsMessage> output)
        {
            log.LogInformation($"{nameof(OrderChangedNotificationFunction)} was triggered.");

            var message = data.ChangedStatus switch
            {
                OrderStatus.Started => $"Ваш заказ #{data.Id} начали готовить.",
                OrderStatus.Ready => $"Ваш заказ #{data.Id} готов.",
                OrderStatus.DeclinedByVenue => $"Ваш заказ #{data.Id} отменен владельцем заведения." + data?.VenueComment,
                OrderStatus.Finished => String.Empty,
                OrderStatus.NotRedeemed => $"Вы не забрали ваш заказ #{data.Id}. Заказ закончен.",
                _ => String.Empty
            };

            if (!String.IsNullOrEmpty(data.UserId) && !String.IsNullOrEmpty(message))
            {
                await output.AddAsync(new HubsMessage(message, Android, "platform:android", $"user:{data.UserId}"));
                await output.AddAsync(new HubsMessage(message, Apple, "platform:apple", $"user:{data.UserId}"));
            }
        }
    }
}
