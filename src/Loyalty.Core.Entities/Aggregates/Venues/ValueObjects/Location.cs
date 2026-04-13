using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Loyalty.Core.Entities.SeedWork;

namespace Loyalty.Core.Entities.Aggregates.Venues.ValueObjects
{
    public class Location : ValueObject
    {
        [MaxLength(200)]
        public string City { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        public float? Latitude { get; set; }

        public float? Longitude { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return City;
            yield return Address;
            yield return Latitude;
            yield return Longitude;
        }
    }
}
