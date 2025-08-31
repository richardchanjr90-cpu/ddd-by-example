using System;
using System.Linq;
using Loyalty.Data.Entities;
using Loyalty.Domain.Handlers.Queries.Commands.VenueDetails;
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
                WorkingHours = command.WorkingHours?.ToString(),
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
                WorkingHours = command.WorkingHours?.ToString(),
                Phones = command.Phones?.ToString()
            };

            return result;
        }

        public static GetVenueDetailsByIdQueryResult ToResult(this VenueDetails item)
        {
            GetVenueDetailsByIdQueryResult result = null;

            if (item != null)
            {
                result = new GetVenueDetailsByIdQueryResult
                {
                    Id = item.Id,
                    VenueId = item.VenueId,
                    Phones = item.Phones?.Split(',').ToList(),
                    PhotosUrl = item.PhotosUrl?.Split(',').ToList(),
                    FullDescription = item.FullDescription,
                    WebSites = item.WebSites?.Split(',').ToList(),
                    WorkingHours = item.WorkingHours?.Split(',').ToList(),
                };
            }

            return result;
        }
    }
}
