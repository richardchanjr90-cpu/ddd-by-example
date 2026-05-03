using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Products;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Products;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Product
{
    public class ProductChangedImageDomainEventHandler :
        INotificationHandler<ProductChangedImageDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public ProductChangedImageDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(ProductChangedImageDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var product = domainEvent.Product;

            await eventBusService.PersistEventAsync(
                new PatchProductImageNotification
                {
                    Id = product.Id,
                    ImageUri = product.ImageUri != null ? new Uri(product.ImageUri) : null,
                });
        }
    }
}