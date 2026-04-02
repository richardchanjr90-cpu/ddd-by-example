using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Notifications.Products;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;
using Loyalty.Infrastructure.DataAccess;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.ProductGroups
{
    public class PatchProductGroupCommandHandler
        : BaseHandler, IRequestHandler<PatchProductGroupCommand, ICommandResult>
    {
        private readonly IMediator mediator;

        public PatchProductGroupCommandHandler(ILoyaltyTenantDbContext context, IMediator mediator, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(PatchProductGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await Context.ProductGroups
                .Include(x => x.Products)
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (group != null)
            {
                foreach (var product in group.Products)
                {
                    if (request.IsAvailableForOrder)
                    {
                        product.ShowToCustomer();
                    }
                    else
                    {
                        product.HideFromCustomer();
                    }
                }
            }

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = group?.Id
            };

            if (group != null && result.Success)
            {
                foreach (var product in group.Products)
                {
                    await mediator.Publish(
                        new PatchProductNotification
                        {
                            Id = product.Id,
                            IsAvailableForOrder = product.IsAvailableForOrder,
                        }, cancellationToken);
                }
            }

            return result;
        }
    }
}