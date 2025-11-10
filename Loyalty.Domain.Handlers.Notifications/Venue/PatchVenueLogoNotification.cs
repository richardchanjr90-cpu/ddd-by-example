using System;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Venue
{
    public class PatchVenueImagesNotification : INotification
    {
        public long Id { get; set; }

        public string Images { get; set; }
    }
}
