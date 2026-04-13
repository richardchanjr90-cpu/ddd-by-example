using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.ProductGroups;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Handlers.Notifications.Products;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.ProductGroup
{
    public class ProductGroupUpdatedDomainEventHandler :
        INotificationHandler<ProductGroupUpdatedDomainEvent>
    {
        private readonly IMediator mediator;
        private readonly IProductRepository productRepository;

        public ProductGroupUpdatedDomainEventHandler(IMediator mediator, IProductRepository productRepository)
        {
            this.mediator = mediator;
            this.productRepository = productRepository;
        }

        public async Task Handle(ProductGroupUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var group = domainEvent.Group;
            var products = await productRepository.GetByGroupAsync(group.Id, cancellationToken);

            foreach (var product in products)
            {
                await mediator.Publish(
                    new UpdateProductNotification
                    {
                        Id = product.Id,
                        GroupIcon = group.Icon,
                        GroupName = group.Name,
                    }, cancellationToken);
            }
        }
    }
}