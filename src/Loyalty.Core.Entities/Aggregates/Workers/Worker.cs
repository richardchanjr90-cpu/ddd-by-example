using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Aggregates.Venues;
using Loyalty.Core.Entities.Base.Interface;
using Loyalty.Core.Entities.Schema;
using Loyalty.Core.Entities.SeedWork;

namespace Loyalty.Core.Entities.Aggregates.Workers
{
    [Table("Worker", Schema = SchemaName.Loyalty)]
    public class Worker : Entity, IAuditableEntity, IArchivableEntity
    {
        public ICollection<VenueWorker> Venues { get; set; }

        public string WorkerId { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhotoUri { get; set; }

        public bool IsArchived { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }
    }
}
