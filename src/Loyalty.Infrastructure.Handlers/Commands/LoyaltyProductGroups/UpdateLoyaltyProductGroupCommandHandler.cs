using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Notifications.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup;
using Loyalty.Infrastructure.DataAccess;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyProductGroups
{
    public class UpdateLoyaltyProductGroupCommandHandler
        : BaseHandler, IUpdateLoyaltyProductGroupCommandHandler
    {
        private readonly IMediator mediator;

        public UpdateLoyaltyProductGroupCommandHandler(ILoyaltyTenantDbContext context, IMediator mediator, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(
            UpdateLoyaltyProductGroupCommand request,
            CancellationToken cancellationToken)
        {
            var group = await Context.LoyaltyProductGroups
                .Include(x => x.LoyaltyProgram)
                .Include(x => x.Group)
                .Include(x => x.Rules)
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var productGroup = await Context.ProductGroups
                .Where(x => x.Id == request.ProductGroupId)
                .SingleAsync(cancellationToken);

            var program = await Context.LoyaltyPrograms
                .SingleAsync(x => x.Id == request.LoyaltyProgramId, cancellationToken);

            if (program.IsPublished)
            {
                throw new LoyaltyValidationException("Impossible to change after program was published.",null, ErrorCode.IS_PUBLISHED);
            }

            if (group == null)
            {
                group = new LoyaltyProductGroup
                {
                    LoyaltyProgramId = request.LoyaltyProgramId,
                    Description = request.Description,
                    Name = request.Name,
                    ProductGroupId = request.ProductGroupId,
                    Group = productGroup,
                    Rules = new List<LoyaltyGroupRule>()
                };

                ProcessRule(request, group);

                Context.LoyaltyProductGroups.Add(group);
            }
            else
            {
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

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = group.Id
            };

            if (result.Success)
            {
                await mediator.Publish(
                    new UpdateLoyaltyProductGroupNotification
                    {
                        Id = group.Id,
                        GroupName = group.Name,
                        Rule = JsonSerializer.Serialize(request.Rule.Rules)
                    },
                    cancellationToken);
            }

            return result;
        }

        private void ProcessRule(UpdateLoyaltyProductGroupCommand request, LoyaltyProductGroup group)
        {
            foreach (var commandRule in request.Rule.Rules)
            {
                var rule = new LoyaltyGroupRule
                {
                    Rule = JsonSerializer.Serialize(commandRule.Rule),
                    RuleVersion = commandRule.RuleVersion,
                    RuleType = commandRule.RuleType
                };

                group.Rules.Add(rule);
            }
        }
    }
}