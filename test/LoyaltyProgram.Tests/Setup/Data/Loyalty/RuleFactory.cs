using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Loyalty.Application.ViewModels.Product;
using Loyalty.Application.ViewModels.Rule;
using Loyalty.Core.Entities.Rules;
using Loyalty.Shared.Contracts.Enums;

namespace LoyaltyProgram.Tests.Setup.Data.Loyalty
{
    public class RuleFactory
    {
        public static RuleViewModel Get()
        {
            var ruleModel = new RuleViewModel();
            var singleRule = new SingleRuleViewModel();
            singleRule.RuleType = LoyaltyRuleType.Stamps;
            singleRule.RuleVersion = LoyaltyRuleVersion.V1;
            singleRule.Rule = "{\"stampsToCollect\": 3,\"stampsToAssign\": 1}";

            ruleModel.Rules = new List<SingleRuleViewModel>();
            ruleModel.Rules.Add(singleRule);

            return ruleModel;
        }
    }
}
