using System.ComponentModel.DataAnnotations;
using Loyalty.Core.Entities.Aggregates.Venues.ValueObjects;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.SeedWork.Interfaces;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Aggregates.Venues
{
    public class Venue : AuditableEntity, 
        IAggregateRoot
    {
        public Venue(
            string name, 
            string ownerId,
            long? parentId,
            Location location,
            VenueDetails details,
            ContactInfo info,
            VenueCategoryType category)
        {
            Name = name;
            OwnerId = ownerId;
            ParentId = parentId;
            Location = location;
            Details = details;
            ContactInfo = info;
            CategoryType = category;
            VenueStatus = VenueApprovalStatus.Saved;
            Type = VenueType.Single;
        }

        private Venue()
        {
           //ef core 
        }

        [Required]
        [MaxLength(200)]
        public string Name { get; private set; }

        [Required]
        public string OwnerId { get; private set; }

        public long? ParentId { get; private set; }

        public Location Location { get; private set; }

        public VenueDetails Details { get; private set; }

        public ContactInfo ContactInfo { get; private set; }

        [MaxLength(200)]
        public string LogoUrl { get; private set; }

        public string Images { get; set; }

        [Required]
        public VenueType Type { get; private set; }

        public VenueCategoryType CategoryType { get; private set; }

        public VenueApprovalStatus VenueStatus { get; private set; }

        public bool AcceptsOrders { get; private set; }

        public bool IsArchived { get; private set; }

        public override long TenantId => Id;

        //public virtual ICollection<LoyaltyProgram> LoyaltyPrograms { get; set; }

        //public virtual ICollection<VenueWorker> Workers { get; set; }

        //public virtual ICollection<ProductGroup> ProductGroups { get; set; }
        public void UpdateVenue(
            string name, 
            Location location, 
            VenueDetails details, 
            ContactInfo info, 
            VenueCategoryType category)
        {
            Name = name;
            Location = location;
            Details = details;
            ContactInfo = info;
            CategoryType = category;
        }

        public void MakePublic()
        {
            VenueStatus = VenueApprovalStatus.Published;
        }

        public void Approve()
        {
            VenueStatus = VenueApprovalStatus.Approved;
        }

        public void Reject()
        {
            VenueStatus = VenueApprovalStatus.Rejected;
        }

        public void AcceptNewOrders()
        {
            AcceptsOrders = true;
        }

        public void RejectNewOrders()
        {
            AcceptsOrders = false;
        }

        public void ChangeLogo(string logo)
        {
            LogoUrl = logo;
        }

        public void ChangePhotos(string images)
        {
            Images = images;
        }

        public void AcceptOrders()
        {
            AcceptsOrders = true;
        }

        public void DeclineOrders()
        {
            AcceptsOrders = false;
        }

        public void Archive()
        {
            IsArchived = true;
        }
    }
}
