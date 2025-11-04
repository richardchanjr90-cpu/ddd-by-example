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
    public class CreateLoyaltyProductGroupCommandHandler
        : BaseHandler, ICreateLoyaltyProductGroupCommandHandler
    {
        public CreateLoyaltyProductGroupCommandHandler(ILoyaltyDbContext context)
            : base(context)
        {
        }

        public async Task<ICommandResult> Handle(CreateLoyaltyProductGroupCommand request, CancellationToken cancellationToken)
        {
            var productGroup = await Context.ProductGroups
                .Where(x => x.Id == request.ProductGroupId)
                .SingleAsync(cancellationToken);

            var group = new LoyaltyProductGroup
            {
                IsArchived = request.IsArchived,
                LoyaltyProgramId = request.LoyaltyProgramId,
                Description = request.Description,
                Name = request.Name,
                Group = productGroup,
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
            }

            Context.LoyaltyProductGroups.Add(group);

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = group.Id
            };

            //if (result.Success)
            //{
            //    await mediator.Publish(venue.ToVenueNotification(), cancellationToken);
            //}
            return result;
        }
    }
}
