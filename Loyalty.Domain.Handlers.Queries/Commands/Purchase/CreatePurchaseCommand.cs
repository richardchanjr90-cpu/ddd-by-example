using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Purchase
{
    public class CreatePurchaseCommand : IRequest<ICommandNotificationResult>
    {
        public long LoyaltyProductGroupId { get; set; }

        public long? ProductId { get; set; }

        public string WorkerId { get; set; }

        public string UserId { get; set; }

        public long VenueId { get; set; }

        public decimal Value { get; set; }
    }
}