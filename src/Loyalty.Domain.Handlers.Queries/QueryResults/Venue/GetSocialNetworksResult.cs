using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Venue
{
    public class GetSocialNetworksResult
    {
        public Uri Instagram { get; set; }

        public Uri Facebook { get; set; }

        public Uri Vkontakte { get; set; }
    }
}
