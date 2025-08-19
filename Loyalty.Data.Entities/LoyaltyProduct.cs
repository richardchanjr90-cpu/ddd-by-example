using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Base.Interface;
using Loyalty.Data.Entities.Schema;
using Microsoft.Build.Framework;

namespace Loyalty.Data.Entities
{
    [Table("LoyaltyProduct", Schema = SchemaName.Loyalty)]
    public class LoyaltyProduct : AuditableEntity, IArchivableEntity
    {
        [ForeignKey(nameof(LoyaltyProductGroup))]
        public long LoyaltyProductGroupId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Rule { get; set; }

        public bool IsArchived { get; set; }
    }
}
