using System;
using Loyalty.Domain.Handlers.Queries.QueryResults.Client;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Purchase
{
    public class GetClientActivePurchasesQuery : IRequest<GetClientActivePurchasesResult>
    {
        public Guid WorkerId { get; set; }

        public Guid UserId { get; set; }

        public long VenueId { get; set; }
    }
}
