using System.Text;
using System.Text.Json;
using MediatR;
using Microsoft.Azure.ServiceBus;

namespace Loyalty.Common.Shared.Extensions
{
    public static class ServiceBusMessageExtensions
    {
        public static T Deserialize<T>(this Message item)
        {
            return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(item.Body));
        }

        public static Message ToMessage(this object item)
        {
            var messageBody = JsonSerializer.Serialize(item, new JsonSerializerOptions()
            {

            });

            var message = new Message(Encoding.UTF8.GetBytes(messageBody))
            {
                ContentType = item.GetType().Name
            };

            return message;
        }
    }
}