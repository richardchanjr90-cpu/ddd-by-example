using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Loyalty.Core.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace LoyaltyProgram.Extensions
{
    public static class HttpRequestExtensions
    {
        public static async Task Authorize(this HttpRequest request, IHost host)
        {
            if (!await request.TryAuthenticate(host))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }
    }
}
