using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Products;
using Loyalty.Domain.Handlers.Queries.Commands.Products;

namespace Loyalty.Infrastructure.Handlers.Commands.Products
{
    public class CreateProductCommandHandler
        : BaseHandler, ICreateProductCommandHandler
    {
        public CreateProductCommandHandler(ILoyaltyDbContext context)
            : base(context)
        {
        }

        public async Task<ICommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Icon = request.Icon,
                Name = request.Name,
                ProductGroupId = request.ProductGroupId
            };

            Context.Products.Add(product);

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = product.Id
            };
        }
    }
}