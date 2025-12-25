using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Worker
{
    public class GetWorkerByPhoneQuery : IRequest<GetInviteByPhoneQueryResult>
    {
        public string Phone { get; set; }
    }
}