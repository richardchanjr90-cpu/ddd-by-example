using System;

namespace Loyalty.Shared.Contracts.Enums
{
    [Flags]
    public enum VenueCategoryType
    {
        CoffeeShop = 1,
        ShawarmaCafe = 2,
        FlowersShop = 4,
        VetShop = 8
    }
}
