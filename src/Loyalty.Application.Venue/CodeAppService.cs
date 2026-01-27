
using System;
using System.Threading.Tasks;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Queries.Code;
using Loyalty.Domain.Handlers.Queries.QueryResults.Code;
using MediatR;

namespace Loyalty.Application.Venue
{
    public class CodeAppService : BaseAppService
    {
        public CodeAppService(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<GetUserInfoByCodeQueryResult> GetByCode(string code)
        {
            if (String.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException(nameof(code));
            }

            var item = await Mediator.Send(new GetUserInfoByCodeQuery
            {
                Code = code
            });

            throw new NotImplementedException();
        }
    }
}