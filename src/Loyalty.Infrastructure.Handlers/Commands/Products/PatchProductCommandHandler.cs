using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Notifications.Products;
using Loyalty.Domain.Handlers.Queries.Commands.Products;
using Loyalty.Infrastructure.DataAccess;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Products
{
    public class PatchProductCommandHandler
        : BaseHandler,  IRequestHandler<PatchProductCommand, ICommandResult>
    {
        private readonly IMediator mediator;

        public PatchProductCommandHandler(ILoyaltyTenantDbContext context, IMediator mediator,  IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(PatchProductCommand request, CancellationToken cancellationToken)
        {
            var product = await Context.Products
                .Include(x => x.ProductGroup)
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (request.IsAvailableForOrder)
            {
                product?.ShowToCustomer();
            }
            else
            {
                product?.HideFromCustomer();
            }

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = product?.Id
            };

            if (product != null && result.Success)
            {
                await mediator.Publish(
                    new PatchProductNotification
                    {
                        Id = product.Id,
                        IsAvailableForOrder = product.IsAvailableForOrder,
                    }, cancellationToken);
            }

            return result;
        }
    }
}