using Loyalty.Core.Entities.Aggregates.Venues;
using MediatR;

namespace Loyalty.Core.Entities.Events.Venues
{
    public class VenueAcceptanceChangedDomainEvent : INotification
    {
        public Venue Venue { get; private set; }

        public VenueAcceptanceChangedDomainEvent(Venue venue)
        {
            Venue = venue;
        }
    }
}
