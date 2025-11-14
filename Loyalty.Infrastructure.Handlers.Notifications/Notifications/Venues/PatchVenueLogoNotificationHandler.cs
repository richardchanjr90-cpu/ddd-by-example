using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Handlers.Notifications.Contracts.Notifications.Venues;
using Loyalty.Domain.Handlers.Notifications.Venue;
using Microsoft.Azure.ServiceBus;

namespace Loyalty.Infrastructure.Handlers.Notifications.Notifications.Venues
{
    public class PatchVenueNotificationHandler: BaseNotificationHandler, IPatchVenueNotificationHandler 
    {
        private readonly IQueueClient client;

        public PatchVenueNotificationHandler(IQueueClient client) 
            : base(client)
        {
            this.client = client;
        }

        public async Task Handle(PatchVenueImagesNotification notification, CancellationToken cancellationToken)
        {
            var message = notification.ToMessage();
            await client.SendAsync(message);
        }
    }
}
