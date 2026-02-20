using System;
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
    public class CreateLoyaltyProductGroupCommandHandler
        : BaseHandler, ICreateLoyaltyProductGroupCommandHandler
    {
        private readonly IMediator mediator;

        public CreateLoyaltyProductGroupCommandHandler(ILoyaltyTenantDbContext context, IMediator mediator, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(CreateLoyaltyProductGroupCommand request, CancellationToken cancellationToken)
        {
            var productGroup = await Context.ProductGroups
                .Where(x => x.Id == request.ProductGroupId)
                .SingleOrDefaultAsync(cancellationToken);

            var loyaltyProgram = await Context.LoyaltyPrograms
                .Where(x => x.Id == request.LoyaltyProgramId)
                .SingleOrDefaultAsync(cancellationToken);

            if (productGroup == null)
            {
                throw new LoyaltyValidationException("No product group with provided id was found.", null, ErrorCode.INCORRECT_PRODUCT_GROUP);
            }

            if (loyaltyProgram.VenueId != productGroup.VenueId)
            {
                throw new LoyaltyValidationException("Product Group and Program belong to different venues.", null, ErrorCode.INCORRECT_PRODUCT_GROUP);
            }

            var group = new LoyaltyProductGroup
            {
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
                    Rule = JsonSerializer.Serialize(commandRule.Rule),
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

            if (result.Success)
            {
                await mediator.Publish(
                    new CreateLoyaltyProductGroupNotification
                    {
                        Id = group.Id,
                        LoyaltyProgramId = group.LoyaltyProgramId,
                        GroupName = group.Name,
                        Rule = JsonSerializer.Serialize(request.Rule.Rules)
                    },
                    cancellationToken);
            }
            return result;
        }
    }
}
