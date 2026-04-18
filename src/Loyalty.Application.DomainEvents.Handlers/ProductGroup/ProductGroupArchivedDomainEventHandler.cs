using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.ProductGroups;
using Loyalty.Core.Entities.Interfaces.Repository;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.ProductGroup
{
    public class ProductGroupArchivedDomainEventHandler :
        INotificationHandler<ProductGroupArchivedDomainEvent>
    {
        private readonly IMediator mediator;
        private readonly IProductRepository productRepository;

        public ProductGroupArchivedDomainEventHandler(IMediator mediator, IProductRepository productRepository)
        {
            this.mediator = mediator;
            this.productRepository = productRepository;
        }

        public async Task Handle(ProductGroupArchivedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var group = domainEvent.Group;

            var products = await productRepository.GetByGroupAsync(group.Id, cancellationToken);

            foreach (var product in products)
            {
                product.Archive();
                productRepository.Update(product);
            }

            //todo: comment when scoped fixed
            await productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
