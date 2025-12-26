using System;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Venue
{
    public class ArchiveVenueNotification : INotification
    {
        public string OwnerId { get; set; }

        public long Id { get; set; }
    }
}