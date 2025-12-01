using System;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Base.Interface;
using Loyalty.Core.Entities.Schema;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities
{
    [Table("Worker", Schema = SchemaName.Loyalty)]
    public class Worker : AuditableEntity, IArchivableEntity
    {
        [ForeignKey(nameof(Venue))]
        public long VenueId { get; set; }

        public string WorkerId { get; set; }

        public VenueUserRole Role { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhotoUri { get; set; }

        public string PositionName { get; set; }

        public bool IsArchived { get; set; }
    }
}
