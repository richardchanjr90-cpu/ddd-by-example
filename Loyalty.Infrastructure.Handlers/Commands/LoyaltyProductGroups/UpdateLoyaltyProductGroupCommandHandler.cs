using System;
using System.Collections.Generic;
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
using Newtonsoft.Json;

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
                .Include(x => x.Group)
                .Include(x => x.Rules)
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var productGroup = await Context.ProductGroups
                .Where(x => x.Id == request.ProductGroupId)
                .SingleAsync(cancellationToken);

            if (group == null)
            {
                group = new LoyaltyProductGroup
                {
                    IsArchived = request.IsArchived,
                    LoyaltyProgramId = request.LoyaltyProgramId,
                    Description = request.Description,
                    Name = request.Name,
                    ProductGroupId = request.ProductGroupId,
                    Group = productGroup,
                    Rules = new List<LoyaltyGroupRule>()
                };

                Context.LoyaltyProductGroups.Add(group);
            }
            else
            {
                group.IsArchived = request.IsArchived;
                group.LoyaltyProgramId = request.LoyaltyProgramId;
                group.Description = request.Description;
                group.Name = request.Name;
                group.ProductGroupId = request.ProductGroupId;
                group.Group = productGroup;
                foreach (var existingChild in group.Rules.ToList())
                {
                    Context.LoyaltyRules.Remove(existingChild);
                }

                ProcessRule(request, group);
            }

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = group.Id
            };
        }

        private void ProcessRule(UpdateLoyaltyProductGroupCommand request, LoyaltyProductGroup group)
        {
            foreach (var commandRule in request.Rule.Rules)
            {
                var rule = new LoyaltyGroupRule
                {
                    Rule = JsonConvert.SerializeObject(commandRule.Rule),
                    RuleVersion = commandRule.RuleVersion,
                    RuleType = commandRule.RuleType
                };
                group.Rules.Add(rule);
            }
        }
    }
}
