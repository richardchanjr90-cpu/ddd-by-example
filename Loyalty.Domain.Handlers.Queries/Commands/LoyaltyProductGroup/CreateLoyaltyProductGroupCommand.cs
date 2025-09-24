using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup
{
    public class CreateLoyaltyProductGroupCommand : IRequest<ICommandResult>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int RuleType { get; set; }

        public string RuleValue { get; set; }

        public bool IsArchived { get; set; }
    }
}
