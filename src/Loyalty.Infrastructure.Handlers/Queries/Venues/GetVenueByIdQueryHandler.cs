using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Location;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.Handlers.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Loyalty.Infrastructure.Handlers.Queries.Venues
{
    public class GetVenueByIdQueryHandler 
        : BaseDapperHandler, IRequestHandler<GetVenueByIdQuery, GetVenueByIdQueryResult>
    {
        private readonly SqlConnection connection;

        public GetVenueByIdQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        public async Task<GetVenueByIdQueryResult> Handle(GetVenueByIdQuery request,
            CancellationToken cancellationToken)
        {
            var id = request.Id;
            var getItems = "SELECT * FROM loyalty.Venue WHERE Id = @id AND IsArchived = 0";
            var dynamicVenue = (await connection.QueryAsync(getItems, new
            {
                id
            })).SingleOrDefault();

            var venue = new GetVenueByIdQueryResult
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
            };

            return venue;
        }
    }
}