using System;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Venue
{
    public class GetWorkersByUserIdQuery : IRequest<GetWorkersByUserIdQueryResult>
    {
        public Guid UserId { get; set; }
    }
}