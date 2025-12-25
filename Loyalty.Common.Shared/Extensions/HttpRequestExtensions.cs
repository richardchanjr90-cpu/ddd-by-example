using System;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;

namespace Loyalty.Common.Shared.Extensions
{
    public static class HttpRequestExtensions
    {
        public static async Task<T> Cast<T>(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var body = await request.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(body);
            return result;
        }

        public static async Task<T> Cast<T>(this HttpRequestMessage request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var body = await request.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(body);
            return result;
        }

        public static Guid ValidateAuthTempGuid(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var headers = request.Headers["X-AUTH-TEMP-GUID"];

            if (!headers.Any())
            {
                throw new ArgumentNullException("request");
            }

            var stringHeader = headers.ToString();

            return Guid.Parse(stringHeader);
        }

        public static void ValidateSecret(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var headers = request.Headers["X-ADMIN-SECRET"];

            if (!headers.Any())
            {
                throw new AuthenticationException("You don't have permission to approve venues");
            }

            var stringHeader = headers.ToString();

            if (stringHeader != "imnotjokingthisoneissecretqwezxc1!@#")
            {
                throw new AuthenticationException("You don't have permission to approve venues");
            }
        }
    }
}