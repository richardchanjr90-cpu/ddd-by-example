using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Products;
using Loyalty.Domain.Handlers.Notifications.Products;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Product
{
    public class ProductAvailabilityChangedDomainEventHandler :
        INotificationHandler<ProductAvailabilityChangedDomainEvent>
    {
        private readonly IMediator mediator;

        public ProductAvailabilityChangedDomainEventHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Handle(ProductAvailabilityChangedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var product = domainEvent.Product;

            await mediator.Publish(
                new PatchProductNotification
                {
                    Id = product.Id,
                    IsAvailableForOrder = product.IsAvailableForOrder,
                }, cancellationToken);
        }
    }
}