using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;

namespace Loyalty.Common.Shared.Extensions
{
    public static class HttpRequestExtensions
    {
        public  static async Task<T> Cast<T>(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var body = await request.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(body);
            return result;
        }
    }
}
