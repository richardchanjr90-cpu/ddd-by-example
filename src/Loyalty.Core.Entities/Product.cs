using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Schema;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities
{
    [Table("Product", Schema = SchemaName.Loyalty)]
    public class Product : AuditableEntity
    {
        //public constructor available to developer to create a new book
        public Product(
            string name,
            ProductIconType? icon,
            string imageUri,
            decimal price,
            string externalUid,
            string description,
            bool isArchived,
            long productGroupId)
        {
            Name = name;
            Icon = icon;
            ImageUri = imageUri;
            Price = price;
            ExternalUid = externalUid;
            IsArchived = isArchived;
            ProductGroupId = productGroupId;
        }

        public Product(
            string name,
            ProductIconType? icon,
            decimal price,
            string externalUid,
            string description,
            long productGroupId)
            : this(name, icon, null, price, externalUid, description,false, productGroupId)
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

        public ProductGroup ProductGroup { get; private set; }

        public override long TenantId => ProductGroup.TenantId;

        public void UpdateProduct(
            string name,
            ProductIconType? icon,
            decimal price,
            string externalUid)
        {
            Name = name;
            Icon = icon;
            Price = price;
            ExternalUid = externalUid;

            CheckPrice();
        }

        public void Archive()
        {
            IsArchived = true;
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
        }

        public void HideFromCustomer()
        {
            IsAvailableForOrder = false;
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
