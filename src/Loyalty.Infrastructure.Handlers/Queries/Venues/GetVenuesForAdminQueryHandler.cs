using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Location;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Loyalty.Infrastructure.Handlers.Queries.Venues
{
    public class GetVenuesForAdminQueryHandler
        : BaseDapperHandler, IRequestHandler<GetVenuesForAdminQuery, GetVenuesByUserIdQueryResult>
    {
        private readonly SqlConnection connection;

        public GetVenuesForAdminQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        public async Task<GetVenuesByUserIdQueryResult> Handle(GetVenuesForAdminQuery request, CancellationToken cancellationToken)
        {
            var getItems = "SELECT * FROM loyalty.Venue WHERE IsArchived = 0";
            var venues = (await connection.QueryAsync(getItems)).ToList();

            var venuesList = venues.Select(dynamicVenue => new GetVenueByIdQueryResult
            {
                Id = dynamicVenue.Id,
                Name = dynamicVenue.Name,
                AcceptsOrders = dynamicVenue.AcceptsOrders,
                OwnerId = dynamicVenue.OwnerId,
                Description = dynamicVenue.Description,
                ParentId = dynamicVenue.ParentId,
                Location = new GetLocationQueryResult()
                {
                    City = dynamicVenue.City,
                    Address = dynamicVenue.Address,
                    Latitude = dynamicVenue.Latitude,
                    Longitude = dynamicVenue.Longitude
                },
                VenueApprovalStatus = dynamicVenue.VenueStatus,
                Type = dynamicVenue.Type,
                CategoryType = dynamicVenue.CategoryType,
                LogoUrl = dynamicVenue.LogoUrl,
                FullDescription = dynamicVenue.FullDescription,
                Phones = ((string)dynamicVenue.Phones).SplitByCommaAndUnwrap() ?? new List<string>(),
                WebSites = ((string)dynamicVenue.WebSites).SplitByCommaAndUnwrap() ?? new List<string>(),
                WorkingHours = JsonSerializer.Deserialize<List<GetVenueWorkingHoursQueryResult>>((string) dynamicVenue.WorkingHours, new JsonSerializerOptions()),
                Images = ((string)dynamicVenue.Images).SplitByCommaAndUnwrap() ?? new List<string>(),
                SocialNetworks = dynamicVenue.SocialNetworks != null ?
                    JsonSerializer.Deserialize<GetSocialNetworksResult>(dynamicVenue.SocialNetworks)
                    : null
            })
                .ToList();

            var result = new GetVenuesByUserIdQueryResult
            {
                Venues = venuesList
            };

            return result;
        }
    }
}