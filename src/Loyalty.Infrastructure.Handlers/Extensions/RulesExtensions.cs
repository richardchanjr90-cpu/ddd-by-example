using System;
using System.Collections.Generic;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Queries.QueryResults.Rules;

namespace Loyalty.Infrastructure.Handlers.Extensions
{
    public static class RulesExtensions
    {
        public static GetSingleRuleByIdQueryResult ToResult(this LoyaltyGroupRule item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var result = new GetSingleRuleByIdQueryResult
            {
                Id = item.Id,
                Rule = item.Rule,
                RuleType = item.RuleType,
                RuleVersion = item.RuleVersion
            };
            return result;
        }

        public static List<GetSingleRuleByIdQueryResult> ToResults(this List<LoyaltyGroupRule> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var results = new List<GetSingleRuleByIdQueryResult>();
            items.ForEach(x => results.Add(x.ToResult()));

            return results;
        }
    }
}