using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Settings;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.Client;
using Loyalty.Domain.Handlers.Queries.Queries.Client;
using Loyalty.Domain.Handlers.Queries.QueryResults.Client;
using Microsoft.Extensions.Options;

namespace Loyalty.Infrastructure.Handlers.Queries.Client
{
    public class GetClientByCodeQueryHandler : BaseHandler, IGetClientByCodeQueryHandler
    {
        public GetClientByCodeQueryHandler(ILoyaltyDbContext context, IOptions<DbSettings> settings)
            : base(context)
        {
        }

        public async Task<GetClientByUserCodeQueryResult> Handle(GetClientByCodeQuery request, CancellationToken cancellationToken)
        {
            //todo: exchange code to guid
            //todo: get a proper avatar
            throw new NotImplementedException();
        }
    }
}
