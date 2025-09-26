using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Worker
{
    public class GetWorkerByIdQuery : IRequest<GetWorkerByIdQueryResult>
    {
        public long Id { get; set; }
    }
}