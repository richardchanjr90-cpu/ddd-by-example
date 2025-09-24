using System.Collections.Generic;
using Loyalty.Domain.Handlers.Queries.Queries.Purchase;
using Loyalty.Domain.Handlers.Queries.QueryResults.Client;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Queries.Client
{
    public interface IGetClientActivePurchasesQueryHandler : IRequestHandler<GetClientActivePurchasesQuery, GetClientActivePurchasesResult>
    {
    }
}
