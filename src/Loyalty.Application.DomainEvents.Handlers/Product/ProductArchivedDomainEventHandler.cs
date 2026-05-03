using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Products;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Products;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Product
{
    public class ProductArchivedDomainEventHandler :
        INotificationHandler<ProductArchivedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public ProductArchivedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(ProductArchivedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await eventBusService.PersistEventAsync(new ArchiveProductNotification
            {
                Id = domainEvent.Product.Id,
                IsArchived = true,
            });

            await eventBusService.PersistEventAsync(new ProductAcceptanceChangedNotification
            {
                Id = domainEvent.Product.VenueId,
            });
        }
    }
}