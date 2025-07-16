using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Domain.Contracts.Interfaces
{
    public interface ICommandResult
    {
        bool Success { get; set; }

        string Message { get; set; }

        int Result { get; set; }
    }
}
