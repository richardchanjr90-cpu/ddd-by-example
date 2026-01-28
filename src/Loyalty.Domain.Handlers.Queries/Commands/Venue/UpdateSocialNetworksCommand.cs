using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Domain.Handlers.Queries.Commands.Venue
{
    public class UpdateSocialNetworksCommand
    {
        public Uri Instagram { get; set; }

        public Uri Facebook { get; set; }

        public Uri Vkontakte { get; set; }
    }
}
