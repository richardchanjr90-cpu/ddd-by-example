using System;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Loyalty.Data.Entities.Base
{
    public abstract class AuditableEntity : Entity
    {
        [BsonElement("createdBy")]
        public Guid CreatedBy { get; set; }

        [BsonElement("modifiedBy")]
        public Guid ModifiedBy { get; set; }

        [BsonElement("modified")]
        public DateTime Modified { get; set; }

        [BsonElement("created")]
        public DateTime Created { get; set; }
    }
}
