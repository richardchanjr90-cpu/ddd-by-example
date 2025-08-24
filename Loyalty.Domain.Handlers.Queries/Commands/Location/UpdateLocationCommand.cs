using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Domain.Handlers.Queries.Commands.Location
{
    public class UpdateLocationCommand
    {
        public long Id { get; set; }

        public long VenueId { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}
