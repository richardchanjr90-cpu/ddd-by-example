using System;

namespace Loyalty.Shared.Contracts.Enums
{
    public enum OrderStatus
    {
        Placed = 1,
        Started = 2,
        Ready = 3,
        Finished = 4,
        DeclinedByVenue = 5,
        DeclinedByCustomer = 6,
        ForceDeclinedByCustomer = 7,
        NotRedeemed = 8
    }
}
