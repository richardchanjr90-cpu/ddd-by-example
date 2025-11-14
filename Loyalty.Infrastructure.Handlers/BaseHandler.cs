using Loyalty.Core.Contracts;

namespace Loyalty.Infrastructure.Handlers
{
    public abstract class BaseHandler
    {
        protected BaseHandler(ILoyaltyDbContext context)
        {
            Context = context;
        }

        public ILoyaltyDbContext Context { get; }
    }
}