using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
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
            var claim2 = principal.Claims.FirstOrDefault(x => x.Type == ClaimConstants.NEW_VENUE_CLAIM)?.Value;

            if (!String.IsNullOrEmpty(claim))
            {
                claims = claim.Split(',')
                    .Select(x=>x.Trim('\"')).ToList();
            }

            if (!String.IsNullOrEmpty(claim2))
            {
                var tempClaims = claim2.Split(',')
                    .Select(x => x.Trim('\"')).ToList();
                claims.AddRange(tempClaims);
            }

            return claims;
        }

        public static ClaimsPrincipal AddVenues(this ClaimsPrincipal principal, long venueId)
        {
            ClaimsIdentity identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(ClaimConstants.NEW_VENUE_CLAIM, venueId.ToString()));
            principal.AddIdentity(identity);
            return principal;
        }

        public static List<long> GetVenueIds(this ClaimsPrincipal principal)
        {
            return GetVenues(principal)?.Select(long.Parse).ToList();
        }

        public static bool IsInRoleAndThrow(this ClaimsPrincipal principal, long id)
        {
            var isInRole = GetVenueIds(principal).Contains(id);
         
            if (!isInRole)
            {
                throw new AuthenticationException();
            }

            return true;
        }
    }
}
