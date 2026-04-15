using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Aggregates.LoyaltyPrograms;
using Loyalty.Core.Entities.Aggregates.ProductGroups;
using Loyalty.Core.Entities.Aggregates.Venues.ValueObjects;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Events.Venues;
using Loyalty.Core.Entities.SeedWork.Interfaces;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Aggregates.Venues
{
    public class Venue : TenantEntity, 
        IAggregateRoot
    {
        public Venue(
            string name, 
            string ownerId,
            Location location,
            VenueDetails details,
            ContactInfo info,
            VenueCategoryType category)
        {
            Name = name;
            OwnerId = ownerId;
            ParentId = null;
            Location = location;
            Details = details;
            ContactInfo = info;
            CategoryType = category;
            VenueStatus = VenueApprovalStatus.Saved;
            Type = VenueType.Single;

            AddDomainEvent(new VenueCreatedDomainEvent(this, ownerId));
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

        public virtual ICollection<LoyaltyProgram> LoyaltyPrograms { get; set; }

        public virtual ICollection<ProductGroup> ProductGroups { get; set; }

        public void UpdateVenue(
            string name, 
            Location location, 
            VenueDetails details, 
            ContactInfo info, 
            VenueCategoryType category,
            VenueApprovalStatus venueApprovalStatus)
        {
            if (VenueStatus != venueApprovalStatus
                && (venueApprovalStatus == VenueApprovalStatus.Approved
                    || venueApprovalStatus == VenueApprovalStatus.Rejected))
            {
                throw new LoyaltyValidationException(
                    "Not possible to change venue's status",
                    ErrorCode.NOT_POSSIBLE_TO_APPROVE_VENUE);
            }

            if (venueApprovalStatus >= VenueApprovalStatus.Published && (
                String.IsNullOrEmpty(Images) ||
                String.IsNullOrEmpty(LogoUrl)))
            {
                throw new LoyaltyValidationException(
                    "Not possible to change venue's status",
                    ErrorCode.NOT_POSSIBLE_TO_PUBLISH_VENUE);
            }

            Name = name;
            Location = location;
            Details = details;
            ContactInfo = info;
            CategoryType = category;

            AddDomainEvent(new VenueUpdatedDomainEvent(this));
        }

        public void MakePublic()
        {
            VenueStatus = VenueApprovalStatus.Published;
        }

        public void Approve()
        {
            if (VenueStatus == VenueApprovalStatus.Saved)
            {
                throw new LoyaltyValidationException(
                    "Venue is not Published, so it can't be approved.", 
                    ErrorCode.FAILED_APPROVE_NOT_PUBLISHED_VENUE);
            }

            VenueStatus = VenueApprovalStatus.Approved;

            AddDomainEvent(new VenueApprovedDomainEvent(this));
        }

        public void Reject()
        {
            if (VenueStatus == VenueApprovalStatus.Saved)
            {
                throw new LoyaltyValidationException(
                    "Venue is not Published, so it can't be rejected.", 
                    ErrorCode.FAILED_REJECT_NOT_PUBLISHED_VENUE);
            }

            VenueStatus = VenueApprovalStatus.Rejected;
        }

        public void AcceptNewOrders()
        {
            AcceptsOrders = true;
            AddDomainEvent(new VenueAcceptanceChangedDomainEvent(this));
        }

        public void RejectNewOrders()
        {
            AcceptsOrders = false;
            AddDomainEvent(new VenueAcceptanceChangedDomainEvent(this));
        }

        public void ChangeLogo(string logo)
        {
            LogoUrl = logo;
            AddDomainEvent(new VenueLogoUpdatedDomainEvent(this));
        }

        public void ChangePhotos(string images)
        {
            Images = images;
            AddDomainEvent(new VenueImagesUpdatedDomainEvent(this));
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
