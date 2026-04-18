using Loyalty.Core.Entities.Aggregates.Workers;
using Loyalty.Shared.Contracts.Enums;
using MediatR;

namespace Loyalty.Core.Entities.Events.Workers
{
    public class WorkerProfileDomainEvent : INotification
    {
        public Worker Worker { get; private set; }

        public WorkerProfileDomainEvent(Worker worker)
        {
            Worker = worker;
        }
    }
}
