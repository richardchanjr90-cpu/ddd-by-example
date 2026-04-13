using System.Security.Claims;
using Loyalty.Core.Contracts;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers
{
    public abstract class BaseHandler
    {
        protected BaseHandler(ILoyaltyDbContext context,  IHttpContextAccessor accessor)
        {
            Context = context;
            Principal = accessor?.HttpContext?.User;
        }

        public ILoyaltyDbContext Context { get; }

        public ClaimsPrincipal Principal { get; }
    }
}