using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Data.Entities;
using Loyalty.Domain.Handlers.Queries.Commands.VenueDetails;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.VenueDetails;

namespace Loyalty.Domain.Handlers.Extensions
{
    public static class VenueDetailsExtensions
    {
        public static VenueDetails ToSingle(this CreateVenueDetailsCommand command)
        {
            var result = new VenueDetails
            {
                VenueId = command.VenueId,
                FullDescription = command.FullDescription,
                PhotosUrl = command.PhotosUrl?.ToString(),
                WebSites = command.WebSites?.ToString(),
                WorkingHours = command.WorkingHours,
                Phones = command.Phones?.ToString()
            };

            return result;
        }

        public static VenueDetails ToSingle(this UpdateVenueDetailsCommand command)
        {
            var result = new VenueDetails
            {
                Id = command.Id,
                VenueId = command.VenueId,
                FullDescription = command.FullDescription,
                PhotosUrl = command.PhotosUrl?.ToString(),
                WebSites = command.WebSites?.ToString(),
                WorkingHours = command.WorkingHours,
                Phones = command.Phones?.ToString()
            };

            return result;
        }

        public static GetVenueDetailsByIdQueryResult ToResult(this VenueDetails item)
        {
            var result = new GetVenueDetailsByIdQueryResult
            {
            };
            return result;
        }
    }
}
