using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
    public class CreateProductCommandHandler
        : BaseHandler, ICreateProductCommandHandler
    {
        private readonly IMediator mediator;

        public CreateProductCommandHandler(ILoyaltyTenantDbContext context, IMediator mediator, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = null;

            var group = await Context.ProductGroups
                .Include(x => x.Products)
                .Where(x => x.Id == request.ProductGroupId)
                .FirstOrDefaultAsync(cancellationToken);

            if (group != null)
            {
                product = new Product(
                    request.Name, 
                    request.Icon, 
                    request.Price, 
                    request.ExternalUid,
                    request.ProductGroupId);

                group.Products.Add(product);
            }

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = product?.Id
            };

            if (result.Success && product != null)
            {
                await mediator.Publish(
                    new CreateProductNotification
                    {
                        Price = product.Price,
                        Id = product.Id,
                        Name = product.Name,
                        GroupIcon = product.ProductGroup.Icon
                    }, cancellationToken);
            }

            return result;
        }
    }
}
