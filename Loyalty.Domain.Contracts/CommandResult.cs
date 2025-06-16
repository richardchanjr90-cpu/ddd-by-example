using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Domain.Contracts.Interfaces;

namespace Loyalty.Domain.Contracts
{
    public class CommandResult : ICommandResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int Result { get; set; }
    }
}
