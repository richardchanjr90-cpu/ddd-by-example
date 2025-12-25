using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Contracts;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.DataAccess
{
    public class TenantTokenProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor accessor;

        public ClaimsPrincipal Principal { get; }

        public TenantTokenProvider(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
            Principal = accessor.HttpContext.User;
        }

        public List<long> GetTenants()
        {
            var ids = accessor.HttpContext.User.GetVenues()
                .Select(x => x.Trim('\"'))
                .Select(long.Parse).ToList();

            return ids;
        }
    }
}
