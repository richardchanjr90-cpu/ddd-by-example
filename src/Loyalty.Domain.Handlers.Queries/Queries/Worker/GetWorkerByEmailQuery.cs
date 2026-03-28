using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Worker
{
    public class GetWorkerByEmailQuery : IRequest<GetInviteByEmailQueryResult>
    {
        public string Email { get; set; }
    }
}