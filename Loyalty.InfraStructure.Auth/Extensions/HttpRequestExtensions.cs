using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Loyalty.InfraStructure.Auth.Extensions
{
    public static class HttpRequestExtensions
    {
        public static async Task AuthorizeAsync(this HttpRequest request, IHost host)
        {
            if (!await request.TryAuthenticate(host))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }
    }
}
