using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Handlers.Notifications.Contracts.Notifications.Venues;
using Loyalty.Domain.ServiceBus.Handlers.Queries.Venue;
using Microsoft.Azure.ServiceBus;

namespace Loyalty.Infrastructure.Handlers.Notifications.Notifications.Venues
{
    public class UpdateVenueNotificationHandler : BaseNotificationHandler, IUpdateVenueNotificationHandler
    {
        private readonly IQueueClient client;

        public UpdateVenueNotificationHandler(IQueueClient client) : base(client)
        {
            this.client = client;
        }

        public async Task Handle(UpdateVenueNotification notification, CancellationToken cancellationToken)
        {
            var message = notification.ToMessage();
            await client.SendAsync(message);
        }
    }
}
