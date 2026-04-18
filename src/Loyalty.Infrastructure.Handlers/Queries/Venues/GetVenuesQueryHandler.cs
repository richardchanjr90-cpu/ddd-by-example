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
            const string getItems = @"SELECT 
                             [Id]
                            ,[Name]
                            ,[OwnerId]
                            ,[ParentId]
                            ,[Location_City] as [City]
                            ,[Location_Address] as [Address]
                            ,[Location_Latitude] as [Latitude]
                            ,[Location_Longitude] as [Longitude]
                            ,[Details_FullDescription] as [FullDescription]
                            ,[Details_Description] as [Description]
                            ,[Details_WorkingHours] as [WorkingHours]
                            ,[ContactInfo_Phones] as [Phones]
                            ,[ContactInfo_WebSites] as [WebSites]
                            ,[ContactInfo_Instagram] as [Instagram]
                            ,[ContactInfo_Facebook] as [Facebook]
                            ,[ContactInfo_Vkontakte] as [Vkontakte]
                            ,[LogoUrl]
                            ,[Images]
                            ,[Type]
                            ,[CategoryType]
                            ,[VenueStatus]
                            ,[AcceptsOrders]
                        FROM [loyalty].[Venue]
                        WHERE Id in @ids AND IsArchived = 0";

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
                Location = new GetLocationQueryResult
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
                Phones = ((string)dynamicVenue.Phones)?.SplitByCommaAndUnwrap() ?? new List<string>(),
                WebSites = ((string)dynamicVenue.WebSites)?.SplitByCommaAndUnwrap() ?? new List<string>(),
                WorkingHours = dynamicVenue.WorkingHours != null ?
                    JsonSerializer.Deserialize<List<GetVenueWorkingHoursQueryResult>>((string)dynamicVenue.WorkingHours, new JsonSerializerOptions()) 
                    : new List<GetVenueWorkingHoursQueryResult>(),
                Images = ((string)dynamicVenue.Images)?.SplitByCommaAndUnwrap() ?? new List<string>(),
                SocialNetworks = new GetSocialNetworksResult
                {
                    Vkontakte = dynamicVenue.Vkontakte,
                    Instagram = dynamicVenue.Instagram,
                    Facebook = dynamicVenue.Facebook
                }
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