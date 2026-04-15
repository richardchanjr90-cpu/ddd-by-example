using Loyalty.Core.Entities.Aggregates.Venues;
using MediatR;

namespace Loyalty.Core.Entities.Events.Venues
{
    public class VenueImagesUpdatedDomainEvent : INotification
    {
        public Venue Venue { get; private set; }

        public VenueImagesUpdatedDomainEvent(Venue venue)
        {
            Venue = venue;
        }
    }
}
