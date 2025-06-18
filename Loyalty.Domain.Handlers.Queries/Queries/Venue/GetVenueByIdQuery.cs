using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Venue
{
    public class GetVenueByIdQuery : IRequest<GetVenueByIdQueryResult>
    {
        public int Id { get; set; }
    }
}