using Loyalty.Core.Entities.Aggregates.ProductGroups;
using Loyalty.Core.Entities.Aggregates.Products;
using MediatR;

namespace Loyalty.Core.Entities.Events.Products
{
    public class ProductCreatedDomainEvent : INotification
    {
        public ProductGroup ProductGroup { get; private set;  }

        public Product Product { get; private set; }

        public ProductCreatedDomainEvent(Product product, ProductGroup group)
        {
            ProductGroup = group;
            Product = product;
        }
    }
}
