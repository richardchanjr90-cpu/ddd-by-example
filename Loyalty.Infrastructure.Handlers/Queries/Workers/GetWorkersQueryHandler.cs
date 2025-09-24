using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.Workers;
using Loyalty.Domain.Handlers.Queries.Queries.Worker;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;

namespace Loyalty.Infrastructure.Handlers.Queries.Workers
{
    public class GetWorkersQueryHandler : BaseHandler, IGetWorkersQueryHandler
    {
        public GetWorkersQueryHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public async Task<GetWorkersQueryResult> Handle(GetWorkersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}