using System;
using System.ComponentModel.DataAnnotations;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Aggregates.ProductGroups;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Events.Products;
using Loyalty.Core.Entities.SeedWork.Interfaces;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Aggregates.Products
{
    public sealed class Product : TenantEntity, IAggregateRoot
    {
        //public constructor available to developer to create a new book
        public Product(
            long id,
            string name,
            ProductIconType? icon,
            string imageUri,
            decimal price,
            string externalUid,
            string description,
            bool isArchived,
            ProductGroup group)
        {
            if (group == null)
            {
                throw new LoyaltyValidationException("Group does not exist.", ErrorCode.INCORRECT_PRODUCT_GROUP);
            }

            ProductGroupId = group.Id;
            VenueId = group.VenueId;
            Id = id;
            Name = name;
            Icon = icon;
            ImageUri = imageUri;
            Price = price;
            Description = description;
            ExternalUid = externalUid;
            IsArchived = isArchived;

            AddDomainEvent(new ProductCreatedDomainEvent(this, group));
        }

        public Product(
            string name,
            ProductIconType? icon,
            decimal price,
            string description,
            ProductGroup group)
            : this(default, name, icon, null, price, null, description, false, group)
        {
        }

        private Product()
        {
        }

        [Required]
        [MaxLength(200)]
        public string Name { get; private set; }

        public ProductIconType? Icon { get; private set; }

        public string ImageUri { get; private set; }

        public decimal Price { get; private set; }

        public bool IsAvailableForOrder { get; private set; }

        public string ExternalUid { get; private set; }

        public string Description { get; private set; }

        public bool IsArchived { get; private set; }

        public long ProductGroupId { get; private set; }

        public long VenueId { get; private set; }

        public override long TenantId => VenueId;

        public void UpdateProduct(
            string name,
            ProductIconType? icon,
            string description,
            decimal price,
            string externalUid)
        {
            Description = description;
            Name = name;
            Icon = icon;
            Price = price;
            ExternalUid = externalUid;

            CheckPrice();
            AddDomainEvent(new ProductUpdatedDomainEvent(this));
        }

        public void Archive()
        {
            IsArchived = true;
            AddDomainEvent(new ProductArchivedDomainEvent(this));
        }

        public void Restore()
        {
            IsArchived = false;
        }

        public void ShowToCustomer()
        {
            IsAvailableForOrder = true;
            CheckPrice();

            if (IsAvailableForOrder && ImageUri == null)
            {
                throw new LoyaltyValidationException(
                    "Impossible to make visible for customer without image.", 
                    ErrorCode.PRODUCT_INVALID_STATE);
            }

            AddDomainEvent(new ProductAvailabilityChangedDomainEvent(this));
        }

        public void HideFromCustomer()
        {
            IsAvailableForOrder = false;
            AddDomainEvent(new ProductAvailabilityChangedDomainEvent(this));
        }

        public void SetImage(string imageUrl)
        {
            if (IsAvailableForOrder && String.IsNullOrEmpty(imageUrl))
            {
                HideFromCustomer();
                ImageUri = null;
            }

            if (!String.IsNullOrEmpty(imageUrl))
            {
                ImageUri = imageUrl;
            }

            AddDomainEvent(new ProductChangedImageDomainEvent(this));
        }

        private void CheckPrice()
        {
            if (IsAvailableForOrder && Price <= 0)
            {
                throw new LoyaltyValidationException(
                    "Impossible to set price <= 0 while available for order by customer.",
                    ErrorCode.PRODUCT_PRICE_INVALID);
            }
        }
    }
}
