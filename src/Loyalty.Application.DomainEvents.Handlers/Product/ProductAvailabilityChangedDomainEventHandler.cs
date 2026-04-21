using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Products;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Products;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Product
{
    public class ProductAvailabilityChangedDomainEventHandler :
        INotificationHandler<ProductAvailabilityChangedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public ProductAvailabilityChangedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(ProductAvailabilityChangedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var product = domainEvent.Product;

            await eventBusService.PersistEventAsync(
                new PatchProductNotification
                {
                    Id = product.Id,
                    IsAvailableForOrder = product.IsAvailableForOrder,
                });

            await eventBusService.PersistEventAsync(new ProductAcceptanceChangedNotification
            {
                Id = domainEvent.Product.VenueId,
            });
        }
    }
}