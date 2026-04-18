using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Commands.Purchases
{
    public class CreateAndBurnPurchaseCommandHandler
        : BaseHandler, IRequestHandler<CreateAndBurnCommand, ICommandNotificationResult>
    {
        private readonly IMediator mediator;

        public CreateAndBurnPurchaseCommandHandler(
            ILoyaltyTenantDbContext context,
            IMediator mediator,
            IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandNotificationResult> Handle(
            CreateAndBurnCommand request,
            CancellationToken cancellationToken)
        {
            var result = new CommandNotificationResult();

            var purchaseResult = await mediator.Send(request.Purchase, cancellationToken);
            var burnResult = await mediator.Send(request.Burn, cancellationToken);

            return result;
        }
    }
}