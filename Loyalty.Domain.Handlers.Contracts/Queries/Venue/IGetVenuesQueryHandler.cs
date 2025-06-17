using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Queries.Venue
{
    public interface IGetVenuesQueryHandler : IRequestHandler<GetVenuesQuery, GetVenuesQueryResult>
    {

    }
}