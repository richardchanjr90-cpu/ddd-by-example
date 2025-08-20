using System;
using System.Collections.Generic;
using Loyalty.Core.Shared.Enums;
using Loyalty.Domain.Handlers.Queries.Commands.VenueCategories;
using Loyalty.Domain.Handlers.Queries.QueryResults.Location;
using Loyalty.Domain.Handlers.Queries.QueryResults.VenueCategory;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Venue
{
    public class GetVenueByIdQueryResult
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public string Description { get; set; }

        public GetLocationQueryResult Location { get; set; }

        public VenueType Type { get; set; }

        public List<GetVenueCategoryQueryResult> Categories { get; set; }

        public string LogoUrl { get; set; }

        public bool IsPublished { get; set; }
    }
}
