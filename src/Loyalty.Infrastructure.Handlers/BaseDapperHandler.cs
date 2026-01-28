using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers
{
    public abstract class BaseDapperHandler
    {
        protected BaseDapperHandler(SqlConnection connection, IHttpContextAccessor accessor)
        {
            Connection = connection;
            Principal = accessor.HttpContext.User;
        }

        public ClaimsPrincipal Principal { get; }

        public IDbConnection Connection { get; }
    }
}