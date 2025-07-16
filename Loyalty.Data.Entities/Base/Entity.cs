using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Loyalty.Data.Entities.Base
{
    public abstract class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public ObjectId InternalId { get; set; }

        [BsonElement("itemId")]
        public Guid ItemId { get; set; }
    }
}