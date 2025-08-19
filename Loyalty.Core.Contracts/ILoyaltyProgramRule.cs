using System;

namespace Loyalty.Core.Contracts
{
    public interface ILoyaltyProgramRule
    {
        string SerializedRule { get; set; }
    }
}
