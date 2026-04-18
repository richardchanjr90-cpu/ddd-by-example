using System;
using System.Threading.Tasks;
using MediatR;

namespace Loyalty.Core.Outbox.Entities.Services
{
    public interface IEventBusService
    {
        Task PersistEventAsync(INotification evt);

        Task PublishEventsAsync(Guid transactionId);
    }
}
