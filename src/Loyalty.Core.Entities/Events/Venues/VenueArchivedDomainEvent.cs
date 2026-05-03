using Loyalty.Core.Entities.Aggregates.Venues;
using MediatR;

namespace Loyalty.Core.Entities.Events.Venues
{
    public class VenueArchivedDomainEvent : INotification
    {
        public Venue Venue { get; private set; }

        public VenueArchivedDomainEvent(Venue venue)
        {
            Venue = venue;
        }
    }
}
