using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Loyalty.Data.Entities.Base
{
    public abstract class Entity
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public ObjectId InternalId { get; set; }

        [BsonElement("itemId")]
        public Guid ItemId { get; set; }
    }
}