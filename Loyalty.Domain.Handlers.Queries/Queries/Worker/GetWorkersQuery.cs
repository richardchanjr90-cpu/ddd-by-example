using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Worker
{
    public class GetWorkersQuery : IRequest<GetWorkersQueryResult>
    {
        public long VenueId { get; set; }
    }
}