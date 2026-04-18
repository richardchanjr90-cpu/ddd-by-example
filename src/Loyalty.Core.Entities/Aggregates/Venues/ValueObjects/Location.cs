using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Loyalty.Core.Entities.SeedWork;

namespace Loyalty.Core.Entities.Aggregates.Venues.ValueObjects
{
    public class Location : ValueObject
    {
        public Location(
            string city, 
            string address, 
            float? latitude,
            float? longitude)
        {
            City = city;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
        } 

        private Location()
        {
        }

        [MaxLength(200)]
        public string City { get; private set; }

        [MaxLength(200)]
        public string Address { get; private set; }

        public float? Latitude { get; private set; }

        public float? Longitude { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return City;
            yield return Address;
            yield return Latitude;
            yield return Longitude;
        }
    }
}
