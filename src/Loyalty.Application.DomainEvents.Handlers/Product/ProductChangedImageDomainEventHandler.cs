using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Products;
using Loyalty.Domain.Handlers.Notifications.Products;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Product
{
    public class ProductChangedImageDomainEventHandler :
        INotificationHandler<ProductChangedImageDomainEvent>
    {
        private readonly IMediator mediator;

        public ProductChangedImageDomainEventHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Handle(ProductChangedImageDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var product = domainEvent.Product;

            await mediator.Publish(
                new PatchProductImageNotification
                {
                    Id = product.Id,
                    ImageUri = product.ImageUri != null ? new Uri(product.ImageUri) : null,
                }, cancellationToken);
        }
    }
}