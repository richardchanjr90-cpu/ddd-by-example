using Loyalty.Data.Entities.Base;

namespace Loyalty.Data.Entities
{
    public class GeoPosition : AuditableEntity
    {
        public string City { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public int NotificationRadius { get; set; }
    }
}