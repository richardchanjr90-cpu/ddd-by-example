using System;
using System.ComponentModel.DataAnnotations;
using Loyalty.Core.Entities.Base.Interface;
using Loyalty.Core.Entities.SeedWork;

namespace Loyalty.Core.Entities.Base
{
    public abstract class AuditableEntity : Entity, IAuditableEntity
    {
        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        [Required]
        public DateTime Modified { get; set; }

        [Required]
        public DateTime Created { get; set; }
    }
}
