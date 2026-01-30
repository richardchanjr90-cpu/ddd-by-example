using System.Collections.Generic;
using System.Text.Json.Serialization;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Rules
{
    public class UpdateRuleCommand : IRequest<ICommandResult>
    {
        [JsonIgnore]
        public long Id { get; set; }

        public List<UpdateSingleRuleCommand> Rules { get; set; } = new List<UpdateSingleRuleCommand>();
    }
}