using Loyalty.Domain.Handlers.Queries.Queries.VenueDetails;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.VenueDetails;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Queries.VenueDetails
{
    public interface IGetVenueDetailsByIdQueryHandler : IRequestHandler<GetVenueDetailsByIdQuery, GetVenueFullByIdQueryResult>
    {
    }
}
