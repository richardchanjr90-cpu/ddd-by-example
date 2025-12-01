using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Worker
{
    public class GetWorkerByPhoneQuery : IRequest<GetWorkerByIdQueryResult>
    {
        public string Phone { get; set; }
    }
}