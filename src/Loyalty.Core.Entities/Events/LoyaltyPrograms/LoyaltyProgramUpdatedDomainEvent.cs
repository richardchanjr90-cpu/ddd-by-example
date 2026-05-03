using Loyalty.Core.Entities.Aggregates.LoyaltyPrograms;
using MediatR;

namespace Loyalty.Core.Entities.Events.LoyaltyPrograms
{
    public class LoyaltyProgramUpdatedDomainEvent: INotification
    {
        public LoyaltyProgram Program { get; private set; }

        public LoyaltyProgramUpdatedDomainEvent(LoyaltyProgram program)
        {
            Program = program;
        }
    }
}
