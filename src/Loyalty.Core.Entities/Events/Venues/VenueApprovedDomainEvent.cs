using Loyalty.Core.Entities.Aggregates.Venues;
using MediatR;

namespace Loyalty.Core.Entities.Events.Venues
{
    public class VenueApprovedDomainEvent : INotification
    {
        public Venue Venue { get; private set; }

        public VenueApprovedDomainEvent(Venue venue)
        {
            Venue = venue;
        }
    }
}
