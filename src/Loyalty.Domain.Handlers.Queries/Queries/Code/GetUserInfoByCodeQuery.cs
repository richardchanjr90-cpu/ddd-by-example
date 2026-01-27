using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Domain.Handlers.Queries.QueryResults.Code;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Code
{
    public class GetUserInfoByCodeQuery : IRequest<GetUserInfoByCodeQueryResult>
    {
        public string Code { get; set; }
    }
}
