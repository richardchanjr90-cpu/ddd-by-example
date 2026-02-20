using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Rules;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup
{
    public class UpdateLoyaltyProductGroupCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }

        public long LoyaltyProgramId { get; set; }

        public string Name { get; set; }

        public UpdateRuleCommand Rule { get; set; }

        public string Description { get; set; }

        public long ProductGroupId { get; set; }
    }
}