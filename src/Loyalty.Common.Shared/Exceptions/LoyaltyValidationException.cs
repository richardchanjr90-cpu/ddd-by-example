using System;
using System.Collections.Generic;

namespace Loyalty.Common.Shared.Exceptions
{
    public class LoyaltyValidationException : Exception
    {
        public List<string> Code { get; } = new List<string>();

        public LoyaltyValidationException(string message, Exception inner, params string [] code)
            : base(message, inner)
        {
            if (code != null)
            {
                Code.AddRange(code);
            }
        }

        public LoyaltyValidationException(string message, params string [] code)
            : base(message)
        {
            if (code != null)
            {
                Code.AddRange(code);
            }
        }
    }
}