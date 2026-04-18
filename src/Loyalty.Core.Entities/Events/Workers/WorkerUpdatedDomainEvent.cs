using Loyalty.Core.Entities.Aggregates.Workers;
using Loyalty.Shared.Contracts.Enums;
using MediatR;

namespace Loyalty.Core.Entities.Events.Workers
{
    public class WorkerUpdatedDomainEvent : INotification
    {
        public Worker Worker { get; private set; }

        public VenueUserRole Role { get; private set; }

        public long VenueId { get; private set; }

        public string PositionName { get; private set; }

        public WorkerUpdatedDomainEvent(Worker worker, VenueUserRole role, long venueId, string positionName)
        {
            Worker = worker;
            Role = role;
            VenueId = venueId;
            PositionName = positionName;
        }
    }
}
