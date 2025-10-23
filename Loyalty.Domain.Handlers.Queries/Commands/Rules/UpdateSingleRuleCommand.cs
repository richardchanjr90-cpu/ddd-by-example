using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Common.Shared.Enums;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Rules
{
    public class UpdateSingleRuleCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }

        public object Rule { get; set; }

        public LoyaltyRuleType RuleType { get; set; }

        public string RuleVersion { get; set; }
    }
}
