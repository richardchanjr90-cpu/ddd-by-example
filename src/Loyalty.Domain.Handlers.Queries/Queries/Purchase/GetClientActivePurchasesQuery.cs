using System;
using Loyalty.Domain.Handlers.Queries.QueryResults.Purchase;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Purchase
{
    public class GetClientActivePurchasesQuery : IRequest<GetActivePurchasesResult>
    {
        public string UserId { get; set; }

        public long VenueId { get; set; }
    }
}