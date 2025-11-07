using Loyalty.Domain.Contracts.Interfaces;

namespace Loyalty.Domain.Contracts
{
    public class CommandResult : ICommandResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }
    }
}