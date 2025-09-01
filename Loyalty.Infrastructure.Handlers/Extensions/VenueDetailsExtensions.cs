using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Queries.Commands.VenueDetails;
using Loyalty.Domain.Handlers.Queries.QueryResults.VenueDetails;

namespace Loyalty.Infrastructure.Handlers.Extensions
{
    public static class VenueDetailsExtensions
    {
        public static VenueDetails ToSingle(this CreateVenueDetailsCommand command)
        {
            var result = new VenueDetails
            {
                VenueId = command.VenueId,
                FullDescription = command.FullDescription,
                PhotosUrl = command.PhotosUrl.ToCommaSeparatedStringOrNull(),
                WebSites = command.WebSites.ToCommaSeparatedStringOrNull(),
                WorkingHours = command.WorkingHours.ToCommaSeparatedStringOrNull(),
                Phones = command.Phones.ToCommaSeparatedStringOrNull(),
            };

            return result;
        }

        public static VenueDetails ToSingle(this UpdateVenueDetailsCommand command)
        {
            var result = new VenueDetails
            {
                VenueId = command.VenueId,
                FullDescription = command.FullDescription,
                PhotosUrl = command.PhotosUrl.ToCommaSeparatedStringOrNull(),
                WebSites = command.WebSites.ToCommaSeparatedStringOrNull(),
                WorkingHours = command.WorkingHours.ToCommaSeparatedStringOrNull(),
                Phones = command.Phones.ToCommaSeparatedStringOrNull(),
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
                    Phones = item.Phones.SplitByCommaAndUnwrap(),
                    PhotosUrl = item.PhotosUrl.SplitByCommaAndUnwrap(),
                    FullDescription = item.FullDescription,
                    WebSites = item.WebSites.SplitByCommaAndUnwrap(),
                    WorkingHours = item.WorkingHours.SplitByCommaAndUnwrap(),
                };
            }

            return result;
        }
    }
}
