using Loyalty.Domain.Handlers.Queries.Queries.Worker;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Queries.Workers
{
    public interface IGetWorkersByUserIdQueryHandler :
        IRequestHandler<GetWorkersByUserIdQuery, GetWorkersByUserIdQueryResult>
    {
    }
}