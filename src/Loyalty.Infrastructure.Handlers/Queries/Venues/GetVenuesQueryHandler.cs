using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Entities;
using Loyalty.Core.Entities.ValueObject;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Infrastructure.Handlers.Extensions;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Loyalty.Infrastructure.Handlers.Queries.Venues
{
    public class GetVenuesQueryHandler 
        : BaseDapperHandler, IRequestHandler<GetVenuesQuery, GetVenuesQueryResult>
    {
        private readonly SqlConnection connection;

        public GetVenuesQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        public Task<GetVenuesQueryResult> Handle(GetVenuesQuery request, CancellationToken cancellationToken)
        {
            var getItems = "SELECT * FROM loyalty.Venue WHERE Id in @ids AND IsArchived = 0";
            var ids = Principal.GetVenueIds();
            var venues = connection.Query(getItems, new
            {
                ids
            }).ToList();

            var venuesList = venues.Select(dynamicVenue => new Venue
            {
                Id = dynamicVenue.Id,
                CreatedBy = dynamicVenue.CreatedBy,
                ModifiedBy = dynamicVenue.ModifiedBy,
                Modified = dynamicVenue.Modified,
                Created = dynamicVenue.Created,
                Name = dynamicVenue.Name,
                AcceptsOrders = dynamicVenue.AcceptsOrders,
                OwnerId = dynamicVenue.OwnerId,
                Description = dynamicVenue.Description,
                ParentId = dynamicVenue.ParentId,
                City = dynamicVenue.City,
                Address = dynamicVenue.Address,
                Latitude = dynamicVenue.Latitude,
                Longitude = dynamicVenue.Longitude,
                Type = (VenueType)dynamicVenue.Type,
                CategoryType = (VenueCategoryType)dynamicVenue.CategoryType,
                LogoUrl = dynamicVenue.LogoUrl,
                FullDescription = dynamicVenue.FullDescription,
                Phones = dynamicVenue.Phones,
                WebSites = dynamicVenue.WebSites,
                WorkingHours = dynamicVenue.WorkingHours,
                Images = dynamicVenue.Images,
                IsArchived = dynamicVenue.IsArchived,
                VenueStatus = (VenueApprovalStatus)dynamicVenue.VenueStatus,
                SocialNetworks = dynamicVenue.SocialNetworks != null ?
                        JsonSerializer.Deserialize<SocialNetworks>(dynamicVenue.SocialNetworks)
                        : null
            })
                .ToList();

            var result = new GetVenuesQueryResult
            {
                Venues = venuesList.ToResults()
            };

            return Task.FromResult(result);
        }
    }
}