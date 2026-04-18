using Loyalty.Core.Entities.Aggregates.Venues;
using MediatR;

namespace Loyalty.Core.Entities.Events.Venues
{
    public class VenueUpdatedDomainEvent : INotification
    {
        public Venue Venue { get; private set; }

        public VenueUpdatedDomainEvent(Venue venue)
        {
            Venue = venue;
        }
    }
}
