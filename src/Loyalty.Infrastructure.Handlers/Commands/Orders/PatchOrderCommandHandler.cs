using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Handlers.Queries.Commands.Orders;
using Loyalty.Infrastructure.DataAccess;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Commands.Orders
{
    public class PatchOrderCommandHandler       
        : BaseHandler, IRequestHandler<PatchOrderCommand, ICommandResult>
    {
        public PatchOrderCommandHandler(ILoyaltyDbContext context, IHttpContextAccessor accessor) 
            : base(context, accessor)
        {
        }

        public Task<ICommandResult> Handle(PatchOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
