using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers
{
    public abstract class BaseDapperHandler
    {
        protected BaseDapperHandler(IDbConnection connection, IHttpContextAccessor accessor)
        {
            Connection = connection;
            Principal = accessor.HttpContext.User;
        }

        public ClaimsPrincipal Principal { get; }

        public IDbConnection Connection { get; }
    }
}