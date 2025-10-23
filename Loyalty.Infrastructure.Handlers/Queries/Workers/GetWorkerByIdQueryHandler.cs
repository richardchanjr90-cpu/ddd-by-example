using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.Workers;
using Loyalty.Domain.Handlers.Queries.Queries.Worker;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using Loyalty.Infrastructure.Handlers.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.Workers
{
    public class GetWorkerByIdQueryHandler : BaseHandler, IGetWorkerByIdQueryHandler
    {
        public GetWorkerByIdQueryHandler(ILoyaltyDbContext context) 
            : base(context)
        {
        }

        public async Task<GetWorkerByIdQueryResult> Handle(GetWorkerByIdQuery request, CancellationToken cancellationToken)
        {
            var worker = await Context.Workers
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            return worker?.ToResult();
        }
    }
}
