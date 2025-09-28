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
        public List<CreateSingleRuleCommand> Rules { get; set; } = new List<CreateSingleRuleCommand>();
    }
}
