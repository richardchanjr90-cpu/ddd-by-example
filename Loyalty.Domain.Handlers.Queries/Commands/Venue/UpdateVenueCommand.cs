using System;
using System.Collections.Generic;
using Loyalty.Common.Shared.Enums.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Locations;
using MediatR;
using Loyalty.Common.Shared.Enums.Contracts;

namespace Loyalty.Domain.Handlers.Queries.Commands.Venue
{
    public class UpdateVenueCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public string Description { get; set; }

        public UpdateLocationCommand Location { get; set; }

        public string FullDescription { get; set; }

        public List<string> Phones { get; set; } = new List<string>();

        public List<string> WebSites { get; set; } = new List<string>();

        public List<string> WorkingHours { get; set; } = new List<string>();

        public long? ParentId { get; set; }

        public VenueType Type { get; set; }

        public VenueCategoryType CategoryType { get; set; }

        public string LogoUrl { get; set; }

        public bool IsArchived { get; set; }

        public bool IsPublished { get; set; }

        public bool IsApproved { get; set; }
    }
}