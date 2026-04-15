using System;
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
            Vkontakte = socialNetworks.Vkontakte;
            Instagram = socialNetworks.Instagram;
            Facebook = socialNetworks.Facebook;
        }

        private ContactInfo()
        {
        }

        public string Phones { get; private set; }

        public string WebSites { get; private set; }

        public Uri Instagram { get; private set; }

        public Uri Facebook { get; private set; }

        public Uri Vkontakte { get; private set; }

        public SocialNetworks SocialNetworks => new SocialNetworks(Instagram, Facebook, Vkontakte);

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Phones;
            yield return WebSites;
            yield return Facebook;
            yield return Vkontakte;
            yield return Instagram;
        }
    }
}
