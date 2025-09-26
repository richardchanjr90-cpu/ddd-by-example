using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyProductGroups
{
    public class UpdateLoyaltyProductGroupCommandHandler
        : BaseHandler, IUpdateLoyaltyProductGroupCommandHandler
    {
        public UpdateLoyaltyProductGroupCommandHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public async Task<ICommandResult> Handle(UpdateLoyaltyProductGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await Context.LoyaltyProductGroups
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (group == null)
            {
                group = new LoyaltyProductGroup
                {
                    IsArchived = request.IsArchived,
                    LoyaltyProgramId = request.LoyaltyProgramId,
                    Description = request.Description,
                    Name = request.Name,
                    //RuleType = request.Rule.RuleType,
                    //ProductGroup = request.ProductGroup.ToResult()
                };

                Context.LoyaltyProductGroups.Add(group);
            }
            else
            {
                group.IsArchived = request.IsArchived;
                group.LoyaltyProgramId = request.LoyaltyProgramId;
                group.Description = request.Description;
                group.Name = request.Name;
                //group.RuleType = request.Rule.RuleType;
                //ProductGroup = request.ProductGroup.ToResult();
            }

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = group.Id
            };
        }
    }
}
