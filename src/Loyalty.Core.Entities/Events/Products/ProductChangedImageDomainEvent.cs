using Loyalty.Core.Entities.Aggregates.Products;
using MediatR;

namespace Loyalty.Core.Entities.Events.Products
{
    public class ProductChangedImageDomainEvent : INotification
    {
        public Product Product { get; private set; }

        public ProductChangedImageDomainEvent(Product product)
        {
            Product = product;
        }
    }
}
