using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Common.Shared.Enums;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Rules
{
    public class CreateRuleCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }

        public LoyaltyRuleType CombinedRuleType { get; set; }

        public List<CreateSingleRuleCommand> Rules { get; set; } = new List<CreateSingleRuleCommand>();
    }
}
