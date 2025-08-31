using System;
using System.Collections.Generic;
using System.Linq;

namespace Loyalty.Core.Shared.Extensions
{
    public static class StringExtensions
    {
        public static List<string> SplitByCommaAndUnwrap(this string value)
        {
            List<string> result = null;

            if (!String.IsNullOrEmpty(value))
            {
                result = value.Split(',').Select(x => x.Trim('\"')).ToList();
            }

            return result;
        }
    }
}
