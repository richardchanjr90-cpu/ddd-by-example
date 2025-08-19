using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Core.Contracts.Abstract
{
    public abstract class LoyaltyProgramRule : ILoyaltyProgramRule
    {
        public string SerializedRule { get; set; }
    }
}
