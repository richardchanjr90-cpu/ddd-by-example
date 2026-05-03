using System;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Purchase
{
    public class CreatePurchaseCommand : IRequest<ICommandResult>
    {
        public long LoyaltyProductGroupId { get; set; }

        public long? ProductId { get; set; }

        public string WorkerId { get; set; }

        public string UserId { get; set; }

        public long VenueId { get; set; }

        public decimal Value { get; set; }
    }
}