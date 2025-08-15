using System;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Schema;

namespace Loyalty.Data.Entities
{
    [Table("VenueDetails", Schema = SchemaName.Loyalty)]
    public class VenueDetails : AuditableEntity
    {
    }
}