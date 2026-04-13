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
    public class GetVenuesQueryHandler 
        : BaseDapperHandler, IRequestHandler<GetVenuesQuery, GetVenuesQueryResult>
    {
        private readonly SqlConnection connection;

        public GetVenuesQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        public async Task<GetVenuesQueryResult> Handle(GetVenuesQuery request, CancellationToken cancellationToken)
        {
            var getItems = "SELECT * FROM loyalty.Venue WHERE Id in @ids AND IsArchived = 0";
            var ids = Principal.GetVenueIds();
            var venues = (await connection.QueryAsync(getItems, new
            {
                ids
            })).ToList();

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
                WorkingHours = JsonSerializer.Deserialize<List<GetVenueWorkingHoursQueryResult>>(dynamicVenue.WorkingHours),
                Images = ((string)dynamicVenue.Images).SplitByCommaAndUnwrap() ?? new List<string>(),
                SocialNetworks = dynamicVenue.SocialNetworks != null ?
                    JsonSerializer.Deserialize<GetSocialNetworksResult>(dynamicVenue.SocialNetworks)
                    : null
            })
                .ToList();

            var result = new GetVenuesQueryResult
            {
                Venues = venuesList
            };

            return result;
        }
    }
}