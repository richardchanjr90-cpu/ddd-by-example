using System;
using System.Threading.Tasks;
using AutoMapper;
using Loyalty.Application.ViewModels.ClientInfo;
using Loyalty.Common.Shared.Settings;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Firebase.Queries.Queries;
using MediatR;
using Microsoft.Extensions.Options;

namespace Loyalty.Application.Venue
{
    public class ClientInfoAppService : BaseAppService
    {
        private readonly IMapper mapper;
        private readonly IOptions<GoogleAuthSettings> googleOptions;

        public ClientInfoAppService(IMediator mediator, IMapper mapper, IOptions<GoogleAuthSettings> googleOptions)
            : base(mediator)
        {
            this.mapper = mapper;
            this.googleOptions = googleOptions;
        }

        public async Task<ClientInfoViewModel> Get(string userId)
        {
            var result = await Mediator.Send(new GetClientInfoFirebaseQuery
            {
                UserId = userId,
                GoogleAuthKey = googleOptions.Value.AuthKey
            });

            return mapper.Map<ClientInfoViewModel>(result);
        }
    }
}