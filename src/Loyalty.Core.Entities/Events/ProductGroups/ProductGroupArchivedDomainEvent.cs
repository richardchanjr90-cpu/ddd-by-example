using Loyalty.Core.Entities.Aggregates.ProductGroups;
using MediatR;

namespace Loyalty.Core.Entities.Events.ProductGroups
{
    public class ProductGroupArchivedDomainEvent : INotification
    {
        public ProductGroup Group { get; private set; }

        public ProductGroupArchivedDomainEvent(ProductGroup group)
        {
            Group = group;
        }
    }
}
