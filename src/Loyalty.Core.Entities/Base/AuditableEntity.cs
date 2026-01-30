using System;
using System.ComponentModel.DataAnnotations;
using Loyalty.Core.Entities.Base.Interface;

namespace Loyalty.Core.Entities.Base
{
    public abstract class AuditableEntity : TenantEntity, IAuditableEntity
    {
        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        [Required]
        public DateTime Modified { get; set; }

        [Required]
        public DateTime Created { get; set; }
    }
}
