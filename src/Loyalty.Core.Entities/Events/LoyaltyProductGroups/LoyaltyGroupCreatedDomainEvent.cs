using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Core.Entities.Aggregates.LoyaltyPrograms;
using MediatR;

namespace Loyalty.Core.Entities.Events.LoyaltyProductGroups
{
    public class LoyaltyGroupCreatedDomainEvent: INotification
    {
        public LoyaltyProductGroup Group { get; private set; }

        public LoyaltyGroupCreatedDomainEvent(LoyaltyProductGroup group)
        {
            Group = group;
        }
    }
}
