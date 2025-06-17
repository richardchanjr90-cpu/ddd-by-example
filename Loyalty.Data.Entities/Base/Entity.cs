using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Loyalty.Data.Entities.Base
{
    public abstract class Entity
    {
        [BsonElement("id")]
        public ObjectId Id { get; set; }

        [BsonElement("itemId")]
        public Guid ItemId { get; set; }
    }
}