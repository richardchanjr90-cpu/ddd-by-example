using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Loyalty.Core.Entities.SeedWork;

namespace Loyalty.Core.Entities.Aggregates.Venues.ValueObjects
{
    public class VenueDetails : ValueObject
    {
        public VenueDetails(
            string fullDescription, 
            string description, 
            string workingHours)
        {
            FullDescription = fullDescription;
            Description = description;
            WorkingHours = workingHours;
        } 

        [MaxLength(4000)]
        public string FullDescription { get; private set; }

        [MaxLength(2000)]
        public string Description { get; private set; }

        public string WorkingHours { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Description;
            yield return FullDescription;
        }
    }
}
