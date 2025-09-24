using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.ProductGroups;
using Loyalty.Domain.Handlers.Queries.Queries.ProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;

namespace Loyalty.Infrastructure.Handlers.Queries.ProductGroups
{
    public class GetProductGroupByIdQueryHandler : BaseHandler, IGetProductGroupByIdQueryHandler
    {
        public GetProductGroupByIdQueryHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public async Task<GetProductGroupByIdQueryResult> Handle(GetProductGroupByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
