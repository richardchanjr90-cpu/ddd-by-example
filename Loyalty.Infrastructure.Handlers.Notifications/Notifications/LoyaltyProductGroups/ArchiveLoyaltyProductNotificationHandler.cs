using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Handlers.Notifications.Contracts.Notifications.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Notifications.LoyaltyProductGroups;
using Microsoft.Azure.ServiceBus;

namespace Loyalty.Infrastructure.Handlers.Notifications.Notifications.LoyaltyProductGroups
{
    public class ArchiveLoyaltyProductNotificationHandler
        : BaseNotificationHandler, IArchiveLoyaltyProductGroupNotificationHandler
    {
        private readonly IQueueClient client;

        public ArchiveLoyaltyProductNotificationHandler(IQueueClient client) : base(client)
        {
            this.client = client;
        }

        public async Task Handle(ArchiveLoyaltyProductGroupNotification notification, CancellationToken cancellationToken)
        {
            var message = notification.ToMessage();
            await client.SendAsync(message);
        }
    }
}
