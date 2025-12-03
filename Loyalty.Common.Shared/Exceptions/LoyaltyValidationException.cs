using System;

namespace Loyalty.Common.Shared.Exceptions
{
    public class LoyaltyValidationException : Exception
    {
        public int Code { get; }

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

        public LoyaltyValidationException(string message, Exception inner, int code)
            : base(message, inner)
        {
            Code = code;
        }
    }
}