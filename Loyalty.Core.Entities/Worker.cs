using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Base.Interface;
using Loyalty.Core.Entities.Schema;

namespace Loyalty.Core.Entities
{
    [Table("Worker", Schema = SchemaName.Loyalty)]
    public class Worker : AuditableEntity, IArchivableEntity
    {
        public ICollection<VenueWorker> Venues { get; set; }

        public string WorkerId { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhotoUri { get; set; }

        public string PositionName { get; set; }

        public bool IsArchived { get; set; }
    }
}
