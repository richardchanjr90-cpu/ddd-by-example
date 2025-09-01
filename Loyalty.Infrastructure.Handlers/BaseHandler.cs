using Loyalty.Core.Contracts;

namespace Loyalty.Infrastructure.Handlers
{
    public abstract class BaseHandler
    {
        public ILoyaltyDbContext Context { get; }

        protected BaseHandler(ILoyaltyDbContext context)
        {
            Context = context;
        }
    }
}
