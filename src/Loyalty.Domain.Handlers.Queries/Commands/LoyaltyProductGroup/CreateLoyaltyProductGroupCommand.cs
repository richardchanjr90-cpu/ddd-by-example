using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Rules;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup
{
    public class CreateLoyaltyProductGroupCommand : IRequest<ICommandResult>
    {
        public long LoyaltyProgramId { get; set; }

        public string Name { get; set; }

        public CreateRuleCommand Rule { get; set; }

        public string Description { get; set; }

        public long ProductGroupId { get; set; }
    }
}