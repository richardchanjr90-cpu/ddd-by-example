using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Venue
{
    public class GetVenueByIdQuery : IRequest<GetVenueByIdQueryResult>
    {
        public long Id { get; set; }
    }
}