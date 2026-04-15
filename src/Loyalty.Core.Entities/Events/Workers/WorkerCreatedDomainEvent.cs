using Loyalty.Core.Entities.Aggregates.Workers;
using MediatR;

namespace Loyalty.Core.Entities.Events.Workers
{
    public class WorkerCreatedDomainEvent : INotification
    {
        public Worker Worker { get; private set; }

        public WorkerCreatedDomainEvent(Worker worker)
        {
            Worker = worker;
        }
    }
}
