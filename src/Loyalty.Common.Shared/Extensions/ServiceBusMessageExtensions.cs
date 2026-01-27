using System.Text;
using MediatR;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace Loyalty.Common.Shared.Extensions
{
    public static class ServiceBusMessageExtensions
    {
        public static T Deserialize<T>(this Message item)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(item.Body));
        }

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