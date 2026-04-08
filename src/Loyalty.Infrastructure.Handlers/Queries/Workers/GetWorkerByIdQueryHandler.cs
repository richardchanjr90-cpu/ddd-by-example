using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Handlers.Queries.Queries.Worker;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.Handlers.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.Workers
{
    public class GetWorkerByIdQueryHandler : BaseHandler, IRequestHandler<GetWorkerByIdQuery, GetWorkerByIdQueryResult>
    {
        public GetWorkerByIdQueryHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<GetWorkerByIdQueryResult> Handle(GetWorkerByIdQuery request, CancellationToken cancellationToken)
        {
            var worker = await Context.Workers
                .Include(x => x.Venues)
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            return worker?.ToResult();
        }
    }
}