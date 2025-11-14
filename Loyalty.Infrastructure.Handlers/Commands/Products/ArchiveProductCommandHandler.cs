using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Products;
using Loyalty.Domain.Handlers.Queries.Commands.Products;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Products
{
    public class ArchiveProductCommandHandler
        : BaseHandler, IArchiveProductCommandHandler
    {
        public ArchiveProductCommandHandler(ILoyaltyDbContext context)
            : base(context)
        {
        }

        public async Task<ICommandResult> Handle(ArchiveProductCommand request, CancellationToken cancellationToken)
        {
            var product = await Context.Products
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (product != null) product.IsArchived = true;

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = product?.Id
            };
        }
    }
}