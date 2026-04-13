using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Loyalty.Core.Entities.SeedWork;

namespace Loyalty.Core.Entities.Aggregates.Venues.ValueObjects
{
    public class VenueDetails : ValueObject
    {
        [MaxLength(4000)]
        public string FullDescription { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        public string WorkingHours { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Description;
            yield return FullDescription;
        }
    }
}
