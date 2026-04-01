using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Base.Interface;
using Loyalty.Core.Entities.Orders;
using Loyalty.Core.Entities.Schema;
using Loyalty.Core.Entities.ValueObject;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities
{
    [Table("Venue", Schema = SchemaName.Loyalty)]
    public class Venue : AuditableEntity, 
        IArchivableEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public string OwnerId { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        public long? ParentId { get; set; }

        [MaxLength(200)]
        public string City { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        public float? Latitude { get; set; }

        public float? Longitude { get; set; }

        [Required]
        public VenueType Type { get; set; }

        public VenueCategoryType CategoryType { get; set; }

        [MaxLength(200)]
        public string LogoUrl { get; set; }

        [MaxLength(4000)]
        public string FullDescription { get; set; }

        public string Phones { get; set; }

        public string WebSites { get; set; }

        public string WorkingHours { get; set; }

        public string Images { get; set; }

        public virtual ICollection<LoyaltyProgram> LoyaltyPrograms { get; set; }

        public virtual ICollection<VenueWorker> Workers { get; set; }

        public virtual ICollection<ProductGroup> ProductGroups { get; set; }

        public virtual ICollection<VenueMenu> Menus { get; set; }

        public bool IsArchived { get; set; }

        public VenueApprovalStatus VenueStatus { get; set; }

        public SocialNetworks SocialNetworks { get; set; }

        public override long TenantId => Id;
    }
}
