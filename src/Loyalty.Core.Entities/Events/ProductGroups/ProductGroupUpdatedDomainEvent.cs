using Loyalty.Core.Entities.Aggregates.ProductGroups;
using MediatR;

namespace Loyalty.Core.Entities.Events.ProductGroups
{
    public class ProductGroupUpdatedDomainEvent : INotification
    {
        public ProductGroup Group { get; private set; }

        public ProductGroupUpdatedDomainEvent(ProductGroup group)
        {
            Group = group;
        }
    }
}
