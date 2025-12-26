using System;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Venue
{
    public class PatchVenueApproveNotification : INotification
    {
        public long Id { get; set; }
    }
}
