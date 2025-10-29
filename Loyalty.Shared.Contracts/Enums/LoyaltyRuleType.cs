using System;

namespace Loyalty.Common.Shared.Enums.Contracts
{
    [Flags]
    public enum LoyaltyRuleType
    {
        Stamps=1,

        PercentFixed=2,

        PercentToDate=4,

        PercentThatDependsOnPeriod=8,

        PercentForActionsDone=16,

        PercentThatDependsOnTotalSum=32,

        BringAFriend=64,

        ActionInASpecifiedDat=128,

        BirthDayPresent=256
    }
}
