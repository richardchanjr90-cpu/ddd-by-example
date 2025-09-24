using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.Workers;
using Loyalty.Domain.Handlers.Queries.Queries.Worker;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;

namespace Loyalty.Infrastructure.Handlers.Queries.Workers
{
    public class GetWorkerByIdQueryHandler : BaseHandler, IGetWorkerByIdQueryHandler
    {
        public GetWorkerByIdQueryHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public async Task<GetWorkerByIdQueryResult> Handle(GetWorkerByIdQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
