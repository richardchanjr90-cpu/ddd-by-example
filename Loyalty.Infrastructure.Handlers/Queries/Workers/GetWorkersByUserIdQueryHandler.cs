using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Enums;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.Workers;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using Loyalty.Infrastructure.Handlers.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.Workers
{
    public class GetWorkersByUserIdQueryHandler : BaseHandler, IGetWorkersByUserIdQueryHandler
    {
        public GetWorkersByUserIdQueryHandler(ILoyaltyDbContext context)
            : base(context)
        {
        }

        public async Task<GetWorkersByUserIdQueryResult> Handle(GetWorkersByUserIdQuery request, CancellationToken cancellationToken)
        {
            //todo: filter by role >= current user role, depends on auth
            var result = await (from workers in Context.Workers
                where workers.WorkerId == request.UserId
                from venue in Context.Venues
                where workers.VenueId == venue.Id
                from allWorkers in Context.Workers
                where allWorkers.VenueId == venue.Id
                select allWorkers).ToListAsync(cancellationToken);

            return new GetWorkersByUserIdQueryResult
            {
                Result = result?.ToResults()
            };
        }
    }
}