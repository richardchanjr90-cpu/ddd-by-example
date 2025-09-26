using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.ProductGroups;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;

namespace Loyalty.Infrastructure.Handlers.Commands.ProductGroups
{
    public class CreateProductGroupCommandHandler
        : BaseHandler, ICreateProductGroupCommandHandler
    {
        public CreateProductGroupCommandHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public async Task<ICommandResult> Handle(CreateProductGroupCommand request, CancellationToken cancellationToken)
        {
            var group = new ProductGroup
            {
                VenueId = request.VenueId,
                Icon = request.Icon,
                Name = request.Name,
            };

            Context.ProductGroups.Add(group);

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = group.Id
            };
        }
    }
}
