using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Commands.Commands.Purchases
{
    public class CreateAndBurnPurchaseCommandHandler
        : IRequestHandler<CreateAndBurnCommand, ICommandResult>
    {
        private readonly IMediator mediator;

        public CreateAndBurnPurchaseCommandHandler(
            IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(
            CreateAndBurnCommand request,
            CancellationToken cancellationToken)
        {
            var purchaseResult = await mediator.Send(request.Purchase, cancellationToken);
            var burnResult = await mediator.Send(request.Burn, cancellationToken);

            return burnResult;
        }
    }
}