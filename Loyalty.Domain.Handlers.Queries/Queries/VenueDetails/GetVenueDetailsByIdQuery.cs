using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.VenueDetails;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.VenueDetails
{
    public class GetVenueDetailsByIdQuery : IRequest<GetVenueDetailsByIdQueryResult>
    {
        public long Id { get; set; }
    }
}