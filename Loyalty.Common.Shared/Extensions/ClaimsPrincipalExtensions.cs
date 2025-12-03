using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Loyalty.Common.Shared.Constants;
using Loyalty.Shared.Contracts.Enums;

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
            var claim = principal.Claims.First(x => x.Type == ClaimTypes.MobilePhone).Value;
            return claim;
        }

        public static string GetSurname(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.First(x => x.Type == ClaimTypes.Surname).Value;
            return claim;
        }

        public static string GetName(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.First(x => x.Type == ClaimTypes.Name).Value;
            return claim;
        }

        public static VenueUserRole GetRole(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.First(x => x.Type == ClaimTypes.Role).Value;
            Enum.TryParse(claim, out VenueUserRole value);
            return value;
        }

        public static List<string> GetVenues(this ClaimsPrincipal principal)
        {
            var claims = new List<string>();
            var claim = principal.Claims.FirstOrDefault(x => x.Type == ClaimConstants.VENUE_CLAIM)?.Value;

            if (!String.IsNullOrEmpty(claim))
            {
                claims = claim.Split(',')
                    .Select(x=>x.Trim('\"')).ToList();
            }

            return claims;
        }

        public static List<long> GetVenueIds(this ClaimsPrincipal principal)
        {
            return GetVenues(principal).Select(long.Parse).ToList();
        }
    }
}
