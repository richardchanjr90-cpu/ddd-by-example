using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup;
using Newtonsoft.Json;

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
                ProductGroupId = request.ProductGroupId
            };

            if (request.Rule == null)
            {
                throw new ArgumentNullException(nameof(request.Rule));
            }

            group.Rules = new List<LoyaltyGroupRule>();

            foreach (var commandRule in request.Rule.Rules)
            {
                var rule = new LoyaltyGroupRule
                {
                    Rule = JsonConvert.SerializeObject(commandRule.Rule),
                    RuleVersion = commandRule.RuleVersion,
                    RuleType = commandRule.RuleType
                };
                group.Rules.Add(rule);
                //rule.ParseRule(commandRule.Rule, commandRule.RuleType);
            }

            Context.LoyaltyProductGroups.Add(group);

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = group.Id
            };
        }
    }
}
