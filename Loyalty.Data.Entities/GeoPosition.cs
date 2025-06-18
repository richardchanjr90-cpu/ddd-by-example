using Loyalty.Data.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Loyalty.Data.Entities
{
    public class GeoPosition : AuditableEntity
    {
        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("latitude")]
        public float Latitude { get; set; }

        [BsonElement("longitude")]
        public float Longitude { get; set; }
    }
}