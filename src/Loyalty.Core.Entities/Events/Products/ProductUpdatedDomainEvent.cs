using Loyalty.Core.Entities.Aggregates.Products;
using MediatR;

namespace Loyalty.Core.Entities.Events.Products
{
    public class ProductUpdatedDomainEvent : INotification
    {
        public Product Product { get; private set; }

        public ProductUpdatedDomainEvent(Product product)
        {
            Product = product;
        }
    }
}
