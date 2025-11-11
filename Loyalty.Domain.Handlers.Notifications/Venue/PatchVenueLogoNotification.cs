using System;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Venue
{
    public class PatchVenueLogoNotification : INotification
    {
        public long Id { get; set; }

        public string Logo { get; set; }
    }
}
