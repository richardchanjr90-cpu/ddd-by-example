using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Entities.Aggregates.Workers;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Handlers.Notifications.Workers;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Notifications.Input
{
    public class WorkerAddedToVenueNotificationHandler
        : INotificationHandler<WorkerAddedToVenueNotification>
    {
        private readonly IWorkerRepository workerRepository;
        private readonly IHttpContextAccessor accessor;

        public WorkerAddedToVenueNotificationHandler(IWorkerRepository workerRepository, IHttpContextAccessor accessor)
        {
            this.workerRepository = workerRepository;
            this.accessor = accessor;
        }


        public async Task Handle(WorkerAddedToVenueNotification notification, CancellationToken cancellationToken)
        {
            var worker = await workerRepository.GetByUidAsync(notification.WorkerId, cancellationToken);
            var venueWorker = new VenueWorker(notification.VenueId, worker.Id, notification.Role, notification.PositionName);
            accessor.HttpContext.User.AddVenues(notification.VenueId);

            worker.AddToVenue(venueWorker);

            workerRepository.Update(worker);

            await workerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}