using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Loyalty.Core.Shared.Extensions
{
    public static class StringExtensions
    {
        private const string RegExpValue = @"([""])(\\?.)*?\1";
        private static readonly Regex ParseQuotes = new Regex(RegExpValue, RegexOptions.Compiled);

        public static List<string> SplitByCommaAndUnwrap(this string value)
        {
            List<string> result = null;

            if (!String.IsNullOrEmpty(value))
            {
                var matchList = ParseQuotes.Matches(value);
                result = matchList.Cast<Match>()
                    .Select(match => match.Value.Trim('\"'))
                    .ToList();
            }

            return result;
        }
    }
}
