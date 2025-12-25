using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Products;
using Loyalty.Domain.Handlers.Queries.Commands.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Products
{
    public class UpdateProductCommandHandler
        : BaseHandler, IUpdateProductCommandHandler
    {
        public UpdateProductCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<ICommandResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await Context.Products
                .Include(x => x.ProductGroup)
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (product == null)
            {
                product = new Product
                {
                    ProductGroupId = request.ProductGroupId,
                    Icon = request.Icon,
                    Name = request.Name
                };

                Context.Products.Add(product);
            }
            else
            {
                product.ProductGroupId = request.ProductGroupId;
                product.Icon = request.Icon;
                product.Name = request.Name;
            }

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = product.Id
            };
        }
    }
}