using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Common.Shared.Extensions
{
    public static class EnvironmentExtensions
    {
        public static bool IsLocal()
        {
            return Environment.GetEnvironmentVariable("FUNCTION_ENV") == "local";
        }

        public static bool IsStage()
        {
            return Environment.GetEnvironmentVariable("FUNCTION_ENV") == "stage";
        }

        public static bool IsProd()
        {
            return Environment.GetEnvironmentVariable("FUNCTION_ENV") == "prod";
        }

    }
}
