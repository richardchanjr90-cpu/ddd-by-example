using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Common.Shared.Enums.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Shared.Contracts.Enums;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Rules
{
    public class UpdateSingleRuleCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }

        public object Rule { get; set; }

        public LoyaltyRuleType RuleType { get; set; }

        public LoyaltyRuleVersion RuleVersion { get; set; }
    }
}
