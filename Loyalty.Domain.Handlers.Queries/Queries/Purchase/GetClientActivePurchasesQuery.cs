using System;
using Loyalty.Domain.Handlers.Queries.QueryResults.Purchase;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Purchase
{
    public class GetClientActivePurchasesQuery : IRequest<GetActivePurchasesResult>
    {
        public Guid UserId { get; set; }

        public long VenueId { get; set; }
    }
}