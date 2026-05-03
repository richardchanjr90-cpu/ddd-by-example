using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Core.Entities.Aggregates.LoyaltyPrograms;
using MediatR;

namespace Loyalty.Core.Entities.Events.LoyaltyPrograms
{
    public class LoyaltyProgramArchivedDomainEvent: INotification
    {
        public LoyaltyProgram Program { get; private set; }

        public LoyaltyProgramArchivedDomainEvent(LoyaltyProgram program)
        {
            Program = program;
        }
    }
}
