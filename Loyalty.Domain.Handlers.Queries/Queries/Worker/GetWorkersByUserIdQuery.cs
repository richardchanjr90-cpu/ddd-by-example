using System;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Worker
{
    public class GetWorkersByUserIdQuery : IRequest<GetWorkersByUserIdQueryResult>
    {
        public string UserId { get; set; }
    }
}