using System;
using Microsoft.Build.Framework;

namespace Loyalty.Core.Entities.Base
{
    public abstract class AuditableEntity : Entity
    {
        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        [Required]
        public DateTime Modified { get; set; }

        [Required]
        public DateTime Created { get; set; }
    }
}
