using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Purchase
{
    public class CreateAndBurnCommand : IRequest<ICommandNotificationResult>
    {
        public BurnPurchaseCommand Burn { get; set; }

        public BurnPurchaseCommand Purchase { get; set; }
    }
}
