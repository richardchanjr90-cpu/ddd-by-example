using System;
using System.Collections.Generic;
using Loyalty.Common.Shared.Enums.Contracts;
using Loyalty.Domain.Handlers.Queries.QueryResults.Location;

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

        public VenueCategoryType CategoryType { get; set; }

        public string FullDescription { get; set; }

        public List<string> Phones { get; set; } = new List<string>();

        public List<string> WebSites { get; set; } = new List<string>();

        public List<GetVenueWorkingHoursQueryResult> WorkingHours { get; set; }
            = new List<GetVenueWorkingHoursQueryResult>();

        public string LogoUrl { get; set; }

        public bool IsPublished { get; set; }

        public bool IsApproved { get; set; }

        public bool IsArchived { get; set; }

        public long? ParentId { get; set; }
    }
}