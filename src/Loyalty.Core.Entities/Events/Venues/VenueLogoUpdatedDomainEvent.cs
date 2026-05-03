using Loyalty.Core.Entities.Aggregates.Venues;
using MediatR;

namespace Loyalty.Core.Entities.Events.Venues
{
    public class VenueLogoUpdatedDomainEvent : INotification
    {
        public Venue Venue { get; private set; }

        public VenueLogoUpdatedDomainEvent(Venue venue)
        {
            Venue = venue;
        }
    }
}
