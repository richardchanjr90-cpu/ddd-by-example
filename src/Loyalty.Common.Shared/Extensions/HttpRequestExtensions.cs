using System;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

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

            var req = request.HttpContext.Request;
            req.Body.Position = 0;
            var result = await JsonSerializer.DeserializeAsync<T>(req.Body);
            req.Body.Position = 0;
            return result;
        }

        public static async Task<T> Cast<T>(this HttpRequest request, ILogger log)
        {
            log.LogWarning("---- Here ----");

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var req = request.HttpContext.Request;
            req.Body.Position = 0;

            log.LogWarning("Request body length: {Body}", req.Body.Length);
            log.LogWarning("Request body length: {Body}", req.Body.Length);
            log.LogWarning("Request body: {@Body}", req.Body);
            log.LogWarning("Request: {@Req}", req);

            var str = request.ReadAsStringAsync();
            log.LogWarning("Request: {str}", str);
            req.Body.Position = 0;
            var result = await JsonSerializer.DeserializeAsync<T>(req.Body);
            req.Body.Position = 0;
            return result;
        }

        public static async Task<T> Cast<T>(this HttpRequestMessage request, ILogger log)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var body = await request.Content.ReadAsStringAsync();

            log.LogWarning("Request body: {@Body}", body);
            log.LogWarning("Request body: {Body}", body);
            log.LogWarning("Request length: {Length}", body.Length);
            log.LogWarning("Request: {@Req}", request.Content);

            var result = JsonSerializer.Deserialize<T>(body);
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