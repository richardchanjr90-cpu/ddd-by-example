using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text.Json;
using System.Text.RegularExpressions;
using Loyalty.Common.Shared.Constants;
using Loyalty.Shared.Contracts.Constants;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Common.Shared.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            var identity = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return identity;
        }

        public static string GetPhone(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone)?.Value;
            return claim;
        }

        public static bool IsAdmin(this ClaimsPrincipal principal)
        {
            var phone = principal?.Claims.First(x => x.Type == ClaimTypes.MobilePhone).Value;
            var allowedPhones = Environment.GetEnvironmentVariable("AdminPhones");
            return !String.IsNullOrEmpty(allowedPhones) && !String.IsNullOrEmpty(phone) && allowedPhones.Contains(phone);
        }

        public static string GetEmailOrNull(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return claim;
        }

        public static string GetSurname(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.First(x => x.Type == CustomClaimsConstants.Lastname)?.Value;
            return Regex.Unescape(claim ?? string.Empty);
        }

        public static string GetName(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.First(x => x.Type == CustomClaimsConstants.Firstname)?.Value;
            return Regex.Unescape(claim ?? string.Empty);
        }

        public static VenueUserRole GetRole(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.First(x => x.Type == ClaimTypes.Role)?.Value;
            Enum.TryParse(claim, out VenueUserRole value);
            return value;
        }

        public static string GetCity(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.FirstOrDefault(x => x.Type == CustomClaimsConstants.City)?.Value;
            return claim;
        }

        public static string GetAvatarOrNull(this ClaimsPrincipal principal)
        {
            //todo: put in a common lib as a const.
            var claim = principal.Claims.FirstOrDefault(x => x.Type == CustomClaimsConstants.ClientPhoto)?.Value;
            return claim;
        }

        public static List<string> GetVenues(this ClaimsPrincipal principal)
        {
            var claims = new List<string>();
            var serialized = principal.Claims.FirstOrDefault(x => x.Type == CustomClaimsConstants.Roles)?.Value;
            var claim2 = principal.Claims.FirstOrDefault(x => x.Type == ClaimConstants.NEW_VENUE_CLAIM)?.Value;

            if(!String.IsNullOrEmpty(serialized))
            {
                var dictionary = JsonSerializer.Deserialize<Dictionary<string, VenueUserRole>>(serialized);
                claims = dictionary.Keys.ToList();
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
            var identity = new ClaimsIdentity();
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
