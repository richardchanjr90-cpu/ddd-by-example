using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Handlers.Notifications.Contracts.Notifications.Venues;
using Loyalty.Domain.ServiceBus.Handlers.Queries.Venue;
using Microsoft.Azure.ServiceBus;

namespace Loyalty.Infrastructure.Handlers.Notifications.Notifications.Venues
{
    public class CreateVenueNotificationHandler : BaseNotificationHandler, ICreateVenueNotificationHandler
    {
        private readonly IQueueClient client;

        public CreateVenueNotificationHandler(IQueueClient client) 
            : base(client)
        {
            this.client = client;
        }

        public async Task Handle(CreateVenueNotification notification, CancellationToken cancellationToken)
        {
            var message = notification.ToMessage();
            await client.SendAsync(message);
        }
    }
}