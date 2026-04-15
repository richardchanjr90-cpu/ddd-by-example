using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Venues;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Venue;
using Loyalty.Shared.Contracts.Enums;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Venues
{
    public class VenueUpdatedDomainEventHandler :
        INotificationHandler<VenueUpdatedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public VenueUpdatedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(VenueUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var item = domainEvent.Venue;

            var result = new UpdateVenueNotification
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
            };

            if (item.ContactInfo.SocialNetworks != null)
            {
                result.SocialNetworks = JsonSerializer.Serialize(item.ContactInfo.SocialNetworks);
            }

            await eventBusService.PersistEventAsync(result);
        }
    }
}
