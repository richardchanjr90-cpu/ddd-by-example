using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Common.Shared.Settings;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Contracts.Queries.Venues;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Infrastructure.Handlers.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.Venues
{
    public class GetVenuesQueryHandler : BaseDapperHandler, IGetVenuesQueryHandler
    {
        private readonly SqlConnection connection;

        public GetVenuesQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        public Task<GetVenuesQueryResult> Handle(GetVenuesQuery request, CancellationToken cancellationToken)
        {
            var getItems = @"SELECT * FROM loyalty.Venue WHERE Id in @ids";
            var ids = Principal.GetVenueIds();
            var venues = connection.Query(getItems, new
            {
                ids
            }).ToList();

            var list = new List<Venue>();

            foreach (var dynamicVenue in venues)
            {
                var ven = new Venue()
                {
                    Id = dynamicVenue.Id,
                    CreatedBy = dynamicVenue.CreatedBy,
                    ModifiedBy = dynamicVenue.ModifiedBy,
                    Modified = dynamicVenue.Modified,
                    Created = dynamicVenue.Created,
                    Name = dynamicVenue.Name,
                    OwnerId = dynamicVenue.OwnerId,
                    Description = dynamicVenue.Description,
                    ParentId = dynamicVenue.ParentId,
                    City = dynamicVenue.City,
                    Address = dynamicVenue.Address,
                    Latitude = dynamicVenue.Latitude,
                    Longitude = dynamicVenue.Longitude,
                    Type = dynamicVenue.Type,
                    CategoryType = dynamicVenue.CategoryType,
                    LogoUrl = dynamicVenue.LogoUrl,
                    FullDescription = dynamicVenue.FullDescription,
                    Phones = dynamicVenue.Phones,
                    WebSites = dynamicVenue.WebSites,
                    WorkingHours = dynamicVenue.WorkingHours,
                    Images = dynamicVenue.Images,
                    IsArchived = dynamicVenue.IsArchived,
                    IsApproved = dynamicVenue.IsApproved,
                    IsPublished = dynamicVenue.IsPublished,
                    //SocialNetworks = dynamicVenue.SocialNetworks
                };
                list.Add(ven);
            }

            var result = new GetVenuesQueryResult
            {
                Venues = list.ToResults()
            };

            return Task.FromResult(result);
        }
    }
}