using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Loyalty.Common.Shared.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            var identity = principal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return identity;
        }

        public static string GetPhone(this ClaimsPrincipal principal)
        {
            var identity = principal.Claims.First(x => x.Type == ClaimTypes.MobilePhone).Value;
            return identity;
        }

        public static string GetSurname(this ClaimsPrincipal principal)
        {
            var identity = principal.Claims.First(x => x.Type == ClaimTypes.Surname).Value;
            return identity;
        }

        public static string GetName(this ClaimsPrincipal principal)
        {
            var identity = principal.Claims.First(x => x.Type == ClaimTypes.Name).Value;
            return identity;
        }
    }
}
