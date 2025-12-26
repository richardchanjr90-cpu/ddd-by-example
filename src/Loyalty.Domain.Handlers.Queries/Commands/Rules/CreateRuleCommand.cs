using System.Collections.Generic;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Rules
{
    public class CreateRuleCommand : IRequest<ICommandResult>
    {
        public List<CreateSingleRuleCommand> Rules { get; set; } = new List<CreateSingleRuleCommand>();
    }
}