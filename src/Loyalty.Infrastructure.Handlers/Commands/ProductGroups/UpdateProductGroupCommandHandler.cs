using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Contracts.Commands.ProductGroups;
using Loyalty.Domain.Handlers.Notifications.Products;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;
using Loyalty.Infrastructure.DataAccess;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.ProductGroups
{
    public class UpdateProductGroupCommandHandler
        : BaseHandler, IUpdateProductGroupCommandHandler
    {
        private readonly IMediator mediator;

        public UpdateProductGroupCommandHandler(ILoyaltyTenantDbContext context, IMediator mediator, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(UpdateProductGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await Context.ProductGroups
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (group != null)
            {
                group.Icon = request.Icon;
                group.Name = request.Name;
            }

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = group?.Id
            };

            if (group?.Products != null && result.Success)
            {
                foreach (var product in group.Products)
                {
                    await mediator.Publish(
                        new UpdateProductNotification
                        {
                            Id = product.Id,
                            GroupIcon = request.Icon,
                            GroupName = request.Name,
                        }, cancellationToken);
                }
            }

            return result;
        }
    }
}