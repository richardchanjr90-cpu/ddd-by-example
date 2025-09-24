using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;
using Newtonsoft.Json;

namespace Loyalty.Domain.Handlers.Queries.Commands.Purchase
{
    public class CreatePurchaseCommand : IRequest<ICommandResult>
    {    
        public long LoyaltyGroupId { get; set; }

        public Guid WorkerId { get; set; }

        public Guid UserId { get; set; }

        public long VenueId { get; set; }

        public decimal Value { get; set; }
    }
}
