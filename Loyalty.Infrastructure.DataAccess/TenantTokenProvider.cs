using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Contracts;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.DataAccess
{
    public class TenantTokenProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor accessor;

        public TenantTokenProvider(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public List<long> GetTentants()
        {
            var ids = accessor.HttpContext.User.GetVenues()
                .Select(x => x.Trim('\"'))
                .Select(long.Parse).ToList();
            return ids;
        }
    }
}
