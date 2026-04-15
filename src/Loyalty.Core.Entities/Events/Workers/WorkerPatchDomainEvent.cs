using Loyalty.Core.Entities.Aggregates.Workers;
using MediatR;

namespace Loyalty.Core.Entities.Events.Workers
{
    public class WorkerPatchDomainEvent : INotification
    {
        public Worker Worker { get; private set; }

        public WorkerPatchDomainEvent(Worker worker)
        {
            Worker = worker;
        }
    }
}
