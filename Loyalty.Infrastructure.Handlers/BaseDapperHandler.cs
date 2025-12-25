using System.Data.SqlClient;
using System.Security.Claims;
using Loyalty.Core.Contracts;
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

        public SqlConnection Connection { get; }
    }
}