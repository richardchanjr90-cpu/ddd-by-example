using System.Collections.Generic;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Rules
{
    public class CreateRuleCommand : IRequest<ICommandResult>
    {
        public List<CreateSingleRuleCommand> Rules { get; set; } = new List<CreateSingleRuleCommand>();
    }
}