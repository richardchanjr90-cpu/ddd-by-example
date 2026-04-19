using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Core.Entities.Aggregates.LoyaltyPrograms;
using MediatR;

namespace Loyalty.Core.Entities.Events.LoyaltyProductGroups
{
    public class LoyaltyGroupArchivedDomainEvent: INotification
    {
        public LoyaltyProductGroup Group { get; private set; }

        public LoyaltyGroupArchivedDomainEvent(LoyaltyProductGroup group)
        {
            Group = group;
        }
    }
}
