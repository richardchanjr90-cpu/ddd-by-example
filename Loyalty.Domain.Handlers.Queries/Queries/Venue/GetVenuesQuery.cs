using System;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Venue
{
    public class GetVenuesQuery : IRequest<GetVenuesQueryResult>
    {
        public string UserId { get; set; }
    }
}