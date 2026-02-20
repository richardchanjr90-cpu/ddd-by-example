using System;
using System.Collections.Generic;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Locations;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Venue
{
    public class UpdateVenueCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public UpdateLocationCommand Location { get; set; }

        public string FullDescription { get; set; }

        public List<string> Phones { get; set; } = new List<string>();

        public List<string> WebSites { get; set; } = new List<string>();

        public List<GetVenueWorkingHoursQueryResult> WorkingHours { get; set; } =
            new List<GetVenueWorkingHoursQueryResult>();

        public CreateSocialNetworksCommand SocialNetworks { get; set; }

        public long? ParentId { get; set; }

        public VenueType Type { get; set; }

        public VenueCategoryType CategoryType { get; set; }

        public VenueApprovalStatus VenueApprovalStatus { get; set; }
    }
}