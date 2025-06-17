using System;
using Loyalty.Data.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Loyalty.Data.Entities
{
    public class LoyaltyProgram : AuditableEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; }

        [BsonElement("startedDate")]
        public DateTime StartedDate { get; set; }

        [BsonElement("endedDate")]
        public DateTime EndedDate { get; set; }

        [BsonElement("isArchived")]
        public bool IsArchived { get; set; }

        [BsonElement("cardBecomesInactiveAfterEnd")]
        public bool CardBecomesInactiveAfterEnd { get; set; }

        [BsonElement("venueId")]
        public int VenueId { get; set; }
    }
}