using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Products;
using Loyalty.Domain.Handlers.Notifications.Products;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Product
{
    public class ProductUpdatedDomainEventHandler :
        INotificationHandler<ProductUpdatedDomainEvent>
    {
        private readonly IMediator mediator;

        public ProductUpdatedDomainEventHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Handle(ProductUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var product = domainEvent.Product;

            await mediator.Publish(
                new UpdateProductNotification
                {
                    Price = product.Price,
                    Id = product.Id,
                    Description = product.Description,
                    Name = product.Name
                }, cancellationToken);
        }
    }
}