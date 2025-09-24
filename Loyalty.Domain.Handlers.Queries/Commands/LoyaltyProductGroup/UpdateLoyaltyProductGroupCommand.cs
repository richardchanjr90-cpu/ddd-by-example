using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup
{
    public class UpdateLoyaltyProductGroupCommand : IRequest<ICommandResult>
    {
        public string Name { get; set; }

        public int RuleType { get; set; }

        public string RuleValue { get; set; }

        public bool IsArchived { get; set; }
    }
}
