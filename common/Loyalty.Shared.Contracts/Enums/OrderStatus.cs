using System;

namespace Loyalty.Shared.Contracts.Enums
{
    [Flags]
    public enum OrderStatus
    {
        Placed = 1,
        Started = 4,
        Ready = 8,
        Finished = 16,
        DeclinedByVenue = 32,
        DeclinedByCustomer = 64
    }
}
