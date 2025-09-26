using System;
using Microsoft.Build.Framework;

namespace Loyalty.Core.Entities.Base
{
    public abstract class AuditableEntity : Entity
    {
        public Guid? CreatedBy { get; set; }

        public Guid? ModifiedBy { get; set; }

        [Required]
        public DateTime Modified { get; set; }

        [Required]
        public DateTime Created { get; set; }
    }
}
