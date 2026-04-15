using System.Collections.Generic;
using Loyalty.Core.Entities.SeedWork;

namespace Loyalty.Core.Entities.Aggregates.Venues.ValueObjects
{
    public class ContactInfo : ValueObject
    {
        public ContactInfo(
            string phones, 
            string webSites, 
            SocialNetworks socialNetworks)
        {
            Phones = phones;
            WebSites = webSites;
            SocialNetworks = socialNetworks;
        } 

        public string Phones { get; private set; }

        public string WebSites { get; private set; }

        public SocialNetworks SocialNetworks { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Phones;
            yield return WebSites;
            yield return SocialNetworks;
        }
    }
}
