using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyProductGroups
{
    public class CreateLoyaltyProductGroupCommandHandler
        : BaseHandler, ICreateLoyaltyProductGroupCommandHandler
    {
        public CreateLoyaltyProductGroupCommandHandler(ILoyaltyDbContext context) 
            : base(context)
        {
        }

        public async Task<ICommandResult> Handle(CreateLoyaltyProductGroupCommand request, CancellationToken cancellationToken)
        {
            var group = new LoyaltyProductGroup
            {
                IsArchived = request.IsArchived,
                LoyaltyProgramId = request.LoyaltyProgramId,
                Description = request.Description,
                Name = request.Name,
                //RuleType = request.Rule.RuleType,
                //ProductGroup = request.ProductGroup.ToResult()
            };

            Context.LoyaltyProductGroups.Add(group);

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = group.Id
            };
        }
    }
}
