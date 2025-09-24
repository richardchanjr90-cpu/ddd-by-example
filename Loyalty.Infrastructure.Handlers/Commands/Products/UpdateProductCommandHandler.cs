using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Products;
using Loyalty.Domain.Handlers.Queries.Commands.Products;

namespace Loyalty.Infrastructure.Handlers.Commands.Products
{
    public class UpdateProductCommandHandler
        : BaseHandler, IUpdateProductCommandHandler
    {
        public UpdateProductCommandHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public Task<ICommandResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
