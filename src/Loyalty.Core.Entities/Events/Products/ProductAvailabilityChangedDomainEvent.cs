using Loyalty.Core.Entities.Aggregates.Products;
using MediatR;

namespace Loyalty.Core.Entities.Events.Products
{
    public class ProductAvailabilityChangedDomainEvent : INotification
    {
        public Product Product { get; private set; }

        public ProductAvailabilityChangedDomainEvent(Product product)
        {
            Product = product;
        }
    }
}
