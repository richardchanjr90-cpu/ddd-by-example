using Loyalty.Core.Entities.Aggregates.ProductGroups;
using MediatR;

namespace Loyalty.Core.Entities.Events.ProductGroups
{
    public class ProductGroupChangedDomainEvent : INotification
    {
        public ProductGroup Group { get; private set; }

        public bool ShowProducts { get; }

        public ProductGroupChangedDomainEvent(ProductGroup group, bool showProducts)
        {
            Group = group;
            ShowProducts = showProducts;
        }
    }
}
