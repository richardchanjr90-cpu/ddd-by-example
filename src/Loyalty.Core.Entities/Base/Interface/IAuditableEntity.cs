using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Loyalty.Core.Entities.Base.Interface
{
    public interface IAuditableEntity
    {
        string CreatedBy { get; set; }

        string ModifiedBy { get; set; }

        [Required]
        DateTime Modified { get; set; }

        [Required]
        DateTime Created { get; set; }
    }
}
