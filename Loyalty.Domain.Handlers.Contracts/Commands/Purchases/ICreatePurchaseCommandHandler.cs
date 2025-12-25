using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Commands.Purchases
{
    public interface ICreatePurchaseCommandHandler 
        : IRequestHandler<CreatePurchaseCommand, ICommandNotificationResult>
    {
    }
}