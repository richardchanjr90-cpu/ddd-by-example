using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.ProductGroups;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;
using Loyalty.Infrastructure.Handlers.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.ProductGroups
{
    public class UpdateProductGroupCommandHandler
        : BaseHandler, IUpdateProductGroupCommandHandler
    {
        public UpdateProductGroupCommandHandler(ILoyaltyDbContext context)
            : base(context)
        {
        }

        public async Task<ICommandResult> Handle(UpdateProductGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await Context.ProductGroups
                .Include(x => x.Products)
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var entities = request.Products?.ToEntities();

            if (group != null)
            {
                // Update parent
                group.VenueId = request.VenueId;
                group.Icon = request.Icon;
                group.Name = request.Name;

                if (entities != null)
                {
                    //Remove some ids
                    foreach (var existingChild in group.Products.ToList())
                    {
                        if (entities.All(c => c.Id != existingChild.Id))
                        {
                            existingChild.ProductGroupId = null;
                            Context.Products.Update(existingChild);
                        }
                    }

                    // Update and Insert children
                    foreach (var childModel in entities)
                    {
                        var existingChild = group.Products.SingleOrDefault(c => c.Id == childModel.Id);

                        if (existingChild != null)
                        {
                            Context.Entry(existingChild).CurrentValues.SetValues(childModel);
                        }
                        else
                        {
                            childModel.ProductGroup = group.Id;
                            Context.Products.Update(childModel);
                        }
                    }
                }
                else
                {
                    //Remove all
                    foreach (var existingChild in group.Products.ToList())
                    {
                        existingChild.ProductGroupId = null;
                        Context.Products.Remove(existexistingChild.);
                    }
                }
            }
            else
            {
                group = new ProductGroup
                {
                    VenueId = request.VenueId,
                    Icon = request.Icon,
                    Name = request.Name,
                    Products = request.Products.ToEntities()
                };
                Context.ProductGroups.Update(group);
            }

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = group?.Id
            };
        }
    }
}
