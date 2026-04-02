using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Contracts.Commands.Products;
using Loyalty.Domain.Handlers.Notifications.Products;
using Loyalty.Domain.Handlers.Queries.Commands.Products;
using Loyalty.Infrastructure.DataAccess;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Products
{
    public class ArchiveProductCommandHandler
        : BaseHandler, IArchiveProductCommandHandler
    {
        private readonly IMediator mediator;

        public ArchiveProductCommandHandler(ILoyaltyTenantDbContext context, IMediator mediator, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(ArchiveProductCommand request, CancellationToken cancellationToken)
        {
            var product = await Context.Products
                .Include(x => x.ProductGroup)
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            product?.Archive();

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = product?.Id
            };

            if (product != null && result.Success)
            {
                await mediator.Publish(
                    new ArchiveProductNotification
                    {
                        Id = product.Id,
                        IsArchived = true,
                    }, cancellationToken);
            }

            return result;
        }
    }
}