using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.Products;
using Loyalty.Domain.Handlers.Queries.Queries.Product;
using Loyalty.Domain.Handlers.Queries.QueryResults.Product;

namespace Loyalty.Infrastructure.Handlers.Queries.Products
{
    public class GetProductByIdQueryHandler : BaseHandler, IGetProductByIdQueryHandler
    {
        public GetProductByIdQueryHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
