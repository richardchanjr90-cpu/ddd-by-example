using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Products;
using Loyalty.Domain.Handlers.Notifications.Products;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Product
{
    public class ProductCreatedDomainEventHandler :
        INotificationHandler<ProductCreatedDomainEvent>
    {
        private readonly IMediator mediator;

        public ProductCreatedDomainEventHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Handle(ProductCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var product = domainEvent.Product;
            var group = domainEvent.ProductGroup;

            await mediator.Publish(
                new CreateProductNotification
                {
                    Price = product.Price,
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    GroupIcon = group.Icon,
                    GroupName = group.Name,
                    VenueId = group.VenueId
                }, cancellationToken);
        }
    }
}