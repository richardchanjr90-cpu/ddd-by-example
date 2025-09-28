using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Common.Shared.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Rules
{
    public class GetRuleByIdQueryResult
    {
        public List<GetSingleRuleByIdQueryResult> Rules { get; set; } = new List<GetSingleRuleByIdQueryResult>();
    }
}
