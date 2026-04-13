using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Core.Entities.SeedWork;

namespace Loyalty.Core.Entities.Aggregates.Venues.ValueObjects
{
    public class ContactInfo : ValueObject
    {
        public string Phones { get; set; }

        public string WebSites { get; set; }

        public SocialNetworks SocialNetworks { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Phones;
            yield return WebSites;
            yield return SocialNetworks;
        }
    }
}
