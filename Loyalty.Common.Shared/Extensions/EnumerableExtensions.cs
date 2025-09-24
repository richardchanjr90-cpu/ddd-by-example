using System.Collections.Generic;
using System.Linq;

namespace Loyalty.Common.Shared.Extensions
{
    public static class EnumerableExtensions
    {
        public static string ToCommaSeparatedStringOrNull(this IEnumerable<string> array)
        {
            string result = null;

            if (array != null)
            {
                result = string.Join(",", array.Select(x => $"\"{x}\""));
            }

            return result;
        }
    }
}
