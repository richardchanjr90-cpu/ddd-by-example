using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Loyalty.Core.Contracts
{
    public interface ITenantProvider
    {
        List<long> GetTentants();

        ClaimsPrincipal Principal { get; }
    }
}
