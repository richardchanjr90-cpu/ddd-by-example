using System;
using MediatR;

namespace Loyalty.Domain.ServiceBus.Handlers.Queries.Venue
{
    public class ArchiveVenueNotification : INotification
    {
        public Guid OwnerId { get; set; }

        public long Id { get; set; }
    }
}
