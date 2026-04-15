using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.Workers;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Handlers.Notifications.Workers;
using MediatR;

namespace Loyalty.Infrastructure.Handlers.Notifications.Input
{
    public class WorkerAddedToVenueNotificationHandler
        : INotificationHandler<WorkerAddedToVenueNotification>
    {
        private readonly IWorkerRepository workerRepository;

        public WorkerAddedToVenueNotificationHandler(IWorkerRepository workerRepository)
        {
            this.workerRepository = workerRepository;
        }


        public async Task Handle(WorkerAddedToVenueNotification notification, CancellationToken cancellationToken)
        {
            var worker = await workerRepository.GetByUidAsync(notification.WorkerId, cancellationToken);
            var venueWorker = new VenueWorker(notification.VenueId, worker.Id, notification.Role, notification.PositionName);

            worker.AddToVenue(venueWorker);

            workerRepository.Update(worker);

            await workerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}