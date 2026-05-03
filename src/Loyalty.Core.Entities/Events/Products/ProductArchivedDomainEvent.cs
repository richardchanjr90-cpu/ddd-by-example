using Loyalty.Core.Entities.Aggregates.Products;
using MediatR;

namespace Loyalty.Core.Entities.Events.Products
{
    public class ProductArchivedDomainEvent : INotification
    {
        public Product Product { get; private set; }

        public ProductArchivedDomainEvent(Product product)
        {
            Product = product;
        }
    }
}
