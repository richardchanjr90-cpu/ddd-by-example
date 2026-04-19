using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Aggregates.LoyaltyPrograms;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyProductGroups
{
    public class CreateLoyaltyProductGroupCommandHandler
        : IRequestHandler<CreateLoyaltyProductGroupCommand, ICommandResult>
    {
        private readonly ILoyaltyProgramRepository programRepository;
        private readonly IProductGroupRepository groupRepository;

        public CreateLoyaltyProductGroupCommandHandler(ILoyaltyProgramRepository programRepository, IProductGroupRepository groupRepository)
        {
            this.programRepository = programRepository;
            this.groupRepository = groupRepository;
        }

        public async Task<ICommandResult> Handle(CreateLoyaltyProductGroupCommand request, CancellationToken cancellationToken)
        {
            var program = await programRepository.GetAsync(request.LoyaltyProgramId, cancellationToken);
            var productGroup = await groupRepository.GetAsync(request.ProductGroupId, cancellationToken);

            if (program == null)
            {
                throw new LoyaltyValidationException("Does not exist.", ErrorCode.INCORRECT_LOYALTY_PROGRAM);
            }

            var group = new LoyaltyProductGroup(
                request.Name,
                productGroup,
                request.Description,
                program.VenueId,
                null);

            foreach (var commandRule in request.Rule.Rules)
            {
                var rule = new LoyaltyGroupRule(commandRule.RuleType, commandRule.Rule, commandRule.RuleVersion);
                group.AddRule(rule);
            }

            program.AddLoyaltyGroup(group);
            programRepository.Update(program);

            var result = new CommandResult
            {
                Success = await programRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = program.Id
            };

            return result;
        }
    }
}
