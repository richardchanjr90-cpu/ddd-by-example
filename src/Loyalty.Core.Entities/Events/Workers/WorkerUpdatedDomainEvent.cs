using Loyalty.Core.Entities.Aggregates.Workers;
using Loyalty.Shared.Contracts.Enums;
using MediatR;

namespace Loyalty.Core.Entities.Events.Workers
{
    public class WorkerUpdatedDomainEvent : INotification
    {
        public Worker Worker { get; private set; }

        public WorkerUpdatedDomainEvent(Worker worker)
        {
            Worker = worker;
        }
    }
}
