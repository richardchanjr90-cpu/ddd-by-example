using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.ProductGroups;
using Loyalty.Core.Entities.Interfaces.Repository;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.ProductGroup
{
    public class ProductGroupChangedDomainEventHandler :
        INotificationHandler<ProductGroupChangedDomainEvent>
    {
        private readonly IProductRepository productRepository;

        public ProductGroupChangedDomainEventHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task Handle(ProductGroupChangedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var group = domainEvent.Group;

            var products = await productRepository.GetByGroupAsync(group.Id, cancellationToken);

            foreach (var product in products)
            {
                if (domainEvent.ShowProducts)
                {
                    product.ShowToCustomer();
                }
                else
                {
                    product.HideFromCustomer();
                }

                productRepository.Update(product);
            }

            //todo: comment when scoped fixed
            await productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}