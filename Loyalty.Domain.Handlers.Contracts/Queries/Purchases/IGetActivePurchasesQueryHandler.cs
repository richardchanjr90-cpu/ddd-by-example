using Loyalty.Domain.Handlers.Queries.Queries.Purchase;
using Loyalty.Domain.Handlers.Queries.QueryResults.Purchase;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Queries.Purchases
{
    public interface IGetClientActivePurchasesQueryHandler : IRequestHandler<GetClientActivePurchasesQuery, GetActivePurchasesResult>
    {
    }
}
