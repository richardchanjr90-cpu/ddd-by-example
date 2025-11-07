using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Handlers.Notifications.Contracts.Notifications.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Notifications.LoyaltyPrograms;
using Microsoft.Azure.ServiceBus;

namespace Loyalty.Infrastructure.Handlers.Notifications.Notifications.LoyaltyPrograms
{
    public class ArchiveLoyaltyProgramNotificationHandler
        : BaseNotificationHandler, IArchiveLoyaltyProgramNotificationHandler
    {
        private readonly IQueueClient client;

        public ArchiveLoyaltyProgramNotificationHandler(IQueueClient client) : base(client)
        {
            this.client = client;
        }

        public async Task Handle(ArchiveLoyaltyProgramNotification notification, CancellationToken cancellationToken)
        {
            var message = notification.ToMessage();
            await client.SendAsync(message);
        }
    }
}