using Loyalty.Data.Contracts;

namespace Loyalty.Domain.Handlers
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
