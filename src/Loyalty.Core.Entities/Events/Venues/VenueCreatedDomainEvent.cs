using Loyalty.Core.Entities.Aggregates.Venues;
using MediatR;

namespace Loyalty.Core.Entities.Events.Venues
{
    public class VenueCreatedDomainEvent : INotification
    {
        public Venue Venue { get; private set; }

        public string WorkerId { get; set; }

        public VenueCreatedDomainEvent(Venue venue, string workerId)
        {
            WorkerId = workerId;
            Venue = venue;
        }
    }
}
