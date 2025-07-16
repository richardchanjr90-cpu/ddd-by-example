using Loyalty.Data.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Loyalty.Data.Entities
{
    public class LoyaltyProduct : AuditableEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("isArchived")]
        public bool IsArchived { get; set; }

        [BsonElement("stampsToCollectCount")]
        public int StampsToCollectCount { get; set; }
    }
}