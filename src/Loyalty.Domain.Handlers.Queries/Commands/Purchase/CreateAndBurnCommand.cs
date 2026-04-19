using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Purchase
{
    public class CreateAndBurnCommand : IRequest<ICommandResult>
    {
        public BurnPurchaseCommand Burn { get; set; }

        public CreatePurchaseCommand Purchase { get; set; }
    }
}
