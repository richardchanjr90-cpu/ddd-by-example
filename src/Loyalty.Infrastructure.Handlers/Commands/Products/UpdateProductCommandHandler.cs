using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities;
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
    public class UpdateProductCommandHandler
        : BaseHandler, IUpdateProductCommandHandler
    {
        private readonly IMediator mediator;

        public UpdateProductCommandHandler(ILoyaltyTenantDbContext context, IMediator mediator,  IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await Context.Products
                .Include(x => x.ProductGroup)
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            product?.UpdateProduct(
                request.Name, 
                request.Icon, 
                request.Price, 
                request.ExternalUid,
                request.ProductGroupId);

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = product?.Id
            };

            if (product != null && result.Success)
            {
                await mediator.Publish(
                    new UpdateProductNotification
                    {
                        Price = product.Price,
                        ImageUri = product.ImageUri,
                        Id = product.Id,
                        IsAvailable = product.IsAvailableForOrder,
                        Name = product.Name,
                        GroupIcon = product.ProductGroup.Icon
                    }, cancellationToken);
            }

            return result;
        }
    }
}