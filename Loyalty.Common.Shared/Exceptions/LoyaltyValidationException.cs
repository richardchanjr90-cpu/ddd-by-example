using System;

namespace Loyalty.Common.Shared.Exceptions
{
    public class LoyaltyValidationException : Exception
    {
        public LoyaltyValidationException()
        {
        }

        public LoyaltyValidationException(string message)
            : base(message)
        {
        }

        public LoyaltyValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
