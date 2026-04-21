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
                OrderStatus.Started => "Ваш заказ начат.",
                OrderStatus.Ready => "Ваш заказ уже готов!",
                OrderStatus.DeclinedByVenue => "Ваш заказ отменен владельцем заведения." + data?.VenueComment,
                OrderStatus.Finished => "Заказ закончен!",
                OrderStatus.NotRedeemed => "Вы не забрали ваш заказ. Заказ закончен.",
                _ => String.Empty
            };

            if (String.IsNullOrEmpty(data.UserId) && !String.IsNullOrEmpty(message))
            {
                await output.AddAsync(new HubsMessage(message, Android, "platform:android", $"user:{data.UserId}"));
                await output.AddAsync(new HubsMessage(message, Apple, "platform:apple", $"user:{data.UserId}"));
            }
        }
    }
}
