using System;
using Loyalty.Data.Contracts;

namespace Loyalty.Domain.Handlers
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
