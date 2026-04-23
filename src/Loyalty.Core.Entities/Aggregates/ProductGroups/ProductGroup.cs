using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Aggregates.Venues;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Events.ProductGroups;
using Loyalty.Core.Entities.SeedWork.Interfaces;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Aggregates.ProductGroups
{
    public class ProductGroup : TenantEntity, IAggregateRoot
    {
        public ProductGroup(
            long venueId,
            string name,
            ProductGroupIconType icon)
        {
            VenueId = venueId;
            Icon = icon;
            Name = name;
        }

        private ProductGroup()
        {
            //ef core
        }

        [ForeignKey(nameof(Venue))] 
        public long VenueId { get; private set; }

        public Venue OwnerVenue { get; set; }

        [Required] 
        [MaxLength(200)] 
        public string Name { get; private set; }

        [Required] 
        public ProductGroupIconType Icon { get; private set; }

        public bool IsArchived { get; private set; }

        public override long TenantId => VenueId;

        public void Archive()
        {
            //if (LoyaltyProductGroups.Any(x => x.ProductGroupId == Id && !x.IsArchived))
            //{
            //    throw new LoyaltyValidationException(
            //        "Cannot delete product group that is assigned to a loyalty group.", 
            //        ErrorCode.INCORRECT_PRODUCT_GROUP);
            //}
            IsArchived = true;
            AddDomainEvent(new ProductGroupArchivedDomainEvent(this));
        }

        public void ShowToCustomer()
        {
            AddDomainEvent(new ProductGroupChangedDomainEvent(this, true));
        }

        public void HideFromCustomer()
        {
            AddDomainEvent(new ProductGroupChangedDomainEvent(this, false));
        }

        public void Update(string name, ProductGroupIconType icon)
        {
            Icon = icon;
            Name = name;

            AddDomainEvent(new ProductGroupUpdatedDomainEvent(this));
        }
    }
}
