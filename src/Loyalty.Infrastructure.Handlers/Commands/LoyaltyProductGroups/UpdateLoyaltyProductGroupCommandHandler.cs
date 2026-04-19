using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.LoyaltyPrograms;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyProductGroups
{
    public class UpdateLoyaltyProductGroupCommandHandler
        : IRequestHandler<UpdateLoyaltyProductGroupCommand, ICommandResult>
    {
        private readonly ILoyaltyProgramRepository programRepository;
        private readonly IProductGroupRepository groupRepository;

        public UpdateLoyaltyProductGroupCommandHandler(
            ILoyaltyProgramRepository programRepository,
            IProductGroupRepository groupRepository)
        {
            this.programRepository = programRepository;
            this.groupRepository = groupRepository;
        }

        public async Task<ICommandResult> Handle(
            UpdateLoyaltyProductGroupCommand request,
            CancellationToken cancellationToken)
        {
            var program = await programRepository.GetAsync(request.LoyaltyProgramId, cancellationToken);
            var productGroup = await groupRepository.GetAsync(request.ProductGroupId, cancellationToken);

            var rules = new List<LoyaltyGroupRule>();

            foreach (var commandRule in request.Rule.Rules)
            {
                var rule = new LoyaltyGroupRule(commandRule.RuleType, commandRule.Rule, commandRule.RuleVersion);
                rules.Add(rule);
            }

            program.UpdateGroup(
                request.Id,
                request.Name,
                productGroup,
                request.Description,
                rules);

            programRepository.Update(program);

            var result = new CommandResult
            {
                Success = await programRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = request.Id
            };

            return result;
        }
    }
}