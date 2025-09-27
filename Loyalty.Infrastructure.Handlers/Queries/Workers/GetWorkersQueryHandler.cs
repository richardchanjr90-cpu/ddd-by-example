using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.Workers;
using Loyalty.Domain.Handlers.Queries.Queries.Worker;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using Loyalty.Infrastructure.Handlers.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.Workers
{
    public class GetWorkersQueryHandler : BaseHandler, IGetWorkersQueryHandler
    {
        public GetWorkersQueryHandler(ILoyaltyDbContext context)
            : base(context)
        {
        }

        public async Task<GetWorkersQueryResult> Handle(GetWorkersQuery request, CancellationToken cancellationToken)
        {
            var workers = await (Context.Workers where).ToListAsync(cancellationToken);

            return new GetWorkersQueryResult
            {
                Result = workers?.ToResults()
            };
        }
    }
}