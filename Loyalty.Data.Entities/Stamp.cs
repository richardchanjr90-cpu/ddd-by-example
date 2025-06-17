using System;
using Loyalty.Data.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Loyalty.Data.Entities
{
    public class Stamp : AuditableEntity
    {
        [BsonElement("cardId")]
        public int CardId { get; set; }

        [BsonElement("stampedDate")]
        public DateTime StampedDate { get; set; }

        [BsonElement("usedDate")]
        public DateTime? UsedDate { get; set; }
    }
}
