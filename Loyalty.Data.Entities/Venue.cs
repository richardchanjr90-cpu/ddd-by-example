using System;
using System.Collections.Generic;
using Loyalty.Core.Shared.Enums;
using Loyalty.Data.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace Loyalty.Data.Entities
{
    public class Venue : AuditableEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("ownerId")]
        public Guid OwnerId { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("location")]
        public GeoPosition Location { get; set; }

        [BsonElement("parentId")]
        public Guid? ParentId { get; set; }

        [BsonElement("type")]
        public VenueType Type { get; set; }

        [BsonElement("category")]
        public VenueCategory Category { get; set; }

        [BsonElement("subsidiaries")]
        public IEnumerable<Venue> Subsidiaries { get; set; }
    }
}