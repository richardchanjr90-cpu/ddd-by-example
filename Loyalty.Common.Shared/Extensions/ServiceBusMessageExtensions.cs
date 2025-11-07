using System.Text;
using MediatR;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace Loyalty.Common.Shared.Extensions
{
    public static class ServiceBusMessageExtensions
    {
        public static Message ToMessage(this INotification item)
        {
            var messageBody = JsonConvert.SerializeObject(item);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody))
            {
                ContentType = item.GetType().Name
            };
            return message;
        }
    }
}