using Loyalty.Core.Entities.Aggregates.Workers;
using MediatR;

namespace Loyalty.Core.Entities.Events.Workers
{
    public class WorkerArchivedDomainEvent : INotification
    {
        public Worker Worker { get; private set; }

        public long VenueId { get; private set; }

        public WorkerArchivedDomainEvent(Worker worker, long venueId)
        {
            Worker = worker;
            VenueId = venueId;
        }
    }
}
