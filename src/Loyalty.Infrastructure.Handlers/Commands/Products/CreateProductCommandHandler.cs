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
    public class CreateProductCommandHandler
        : BaseHandler, ICreateProductCommandHandler
    {
        public CreateProductCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
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
                product = new Product
                {
                    Icon = request.Icon,
                    Name = request.Name,
                    ProductGroupId = request.ProductGroupId,
                };

                group.Products.Add(product);
            }

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = product?.Id
            };
        }
    }
}
