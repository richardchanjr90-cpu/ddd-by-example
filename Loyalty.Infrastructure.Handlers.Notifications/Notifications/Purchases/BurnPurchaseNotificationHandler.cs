using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Handlers.Notifications.Contracts.Notifications.Purchases;
using Loyalty.Domain.Handlers.Notifications.Purchases;
using Microsoft.Azure.ServiceBus;

namespace Loyalty.Infrastructure.Handlers.Notifications.Notifications.Purchases
{
    public class BurnPurchaseNotificationHandler
        : BaseNotificationHandler, IBurnPurchaseNotificationHandler
    {
        private readonly IQueueClient client;

        public BurnPurchaseNotificationHandler(IQueueClient client) : base(client)
        {
            this.client = client;
        }

        public async Task Handle(BurnPurchaseNotification notification, CancellationToken cancellationToken)
        {
            var message = notification.ToMessage();
            await client.SendAsync(message);
        }
    }
}