using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Entities.Events.Venues;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Venue;
using Loyalty.Domain.Handlers.Notifications.Workers;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Application.DomainEvents.Handlers.Venues
{
    public class VenueCreatedDomainEventHandler :
        INotificationHandler<VenueCreatedDomainEvent>
    {
        private readonly IEventBusService eventBusService;
        private readonly IWorkerRepository workerRepository;
        private readonly IHttpContextAccessor accessor;

        public VenueCreatedDomainEventHandler(IEventBusService eventBusService, 
            IWorkerRepository workerRepository, IHttpContextAccessor accessor)
        {
            this.eventBusService = eventBusService;
            this.workerRepository = workerRepository;
            this.accessor = accessor;
        }

        public async Task Handle(VenueCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {

            var item = domainEvent.Venue;
            accessor.HttpContext.User.AddVenues(item.Id);

            var worker = await workerRepository.GetByUidAsync(domainEvent.WorkerId, cancellationToken);

            var ids = worker.VenueRoles
                .Select(x => x.VenueId)
                .Select(x => x.ToString())
                .ToList();

            await eventBusService.PersistEventAsync(new WorkerAddedToVenueNotification
            {
                VenueId = domainEvent.Venue.Id,
                PositionName = "Владелец",
                Role =  VenueUserRole.Owner,
                WorkerId = domainEvent.WorkerId
            });

            await eventBusService.PersistEventAsync(new CreateVenueNotification
            {
                Id = item.Id,
                CategoryType = item.CategoryType,
                Description = item.Details.Description,
                Name = item.Name,
                Type = item.Type,
                OwnerId = item.OwnerId,
                Latitude = item.Location.Latitude,
                Longitude = item.Location.Longitude,
                City = item.Location.City,
                Address = item.Location.Address,
                IsPublished = item.VenueStatus >= VenueApprovalStatus.Published,
                LogoUrl = item.LogoUrl,
                Phones = item.ContactInfo.Phones,
                FullDescription = item.Details.FullDescription,
                WebSites = item.ContactInfo.WebSites,
                WorkingHours = item.Details.WorkingHours,
                IsArchived = item.IsArchived,
                IsApproved = item.VenueStatus == VenueApprovalStatus.Approved,
                ParentId = item.ParentId,
            });

            await eventBusService.PersistEventAsync(new UpdatedWorkerNotification
            {
                WorkerId = worker.WorkerId,
                LastName = worker.LastName,
                Name = worker.Name,
                PhotoUri = worker.PhotoUri,
                Role = VenueUserRole.Owner,
                VenueId = item.Id
            });

            await eventBusService.PersistEventAsync(new AddUserToVenueNotification
            {
                UserId = domainEvent.WorkerId,
                VenueIds = ids
            });
        }
    }
}
