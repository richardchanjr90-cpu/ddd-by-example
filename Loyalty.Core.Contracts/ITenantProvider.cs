using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Core.Contracts
{
    public interface ITenantProvider
    {
        List<long> GetTentants();
    }
}
