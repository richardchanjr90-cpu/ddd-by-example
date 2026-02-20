using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Contracts.Interfaces
{
    public interface ICommandNotificationResult
    {
        IList<Func<INotification>> OnSuccessNotifications { get; set; }

        IList<Func<INotification>> OnFailNotifications { get; set; }

        ICommandResult CommandResult { get; set; }

        string Message { get; set; }

        object Result { get; set; }
    }
}
