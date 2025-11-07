using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Purchase
{
    public class BurnPurchaseCommand : IRequest<ICommandResult>
    {
        public long LoyaltyProductGroupId { get; set; }

        public Guid WorkerId { get; set; }

        public Guid UserId { get; set; }

        public long VenueId { get; set; }

        public decimal Amount { get; set; }
    }
}